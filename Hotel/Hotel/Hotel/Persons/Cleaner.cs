using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework;

namespace Hotel.Persons
{
    public class Cleaner : Person
    {
        private float _cleaningTimer;
        private float _cleaningDuration;
        private bool _isCleaning;
        private bool _isBusy;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="room">The room to spawn the guest in.</param>
        public Cleaner(Room room) : base(room)
        {
            Name = "Tim";
            Sprite.LoadSprite("Cleaner");
            _isCleaning = false;

            // Load settings from the config.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();
            _cleaningDuration = (float)config.CleaningDuration;
            _cleaningTimer = _cleaningDuration;

            Arrival += Cleaner_Arrival;
            Death += Cleaner_Death;
        }

        private void Cleaner_Death(object sender, EventArgs e)
        {
            // Unclaim the room which the cleaner was supposed to clean.
            if (TargetRoom != null && TargetRoom.State == RoomState.InCleaning)
                TargetRoom.State = TargetRoom.PrevRoomState;

            // Set an emergency in this room.
            if (CurrentRoom.State != RoomState.Emergency && CurrentRoom.State != RoomState.InCleaning)
                CurrentRoom.SetEmergency(8);
        }

        private void Cleaner_Arrival(object sender, EventArgs e)
        {
            // Cleaner has arrived at the dirty room.
            // If the room where the cleaner has arrived at is dirty set _isCleaning to true.
            if(TargetRoom.State == RoomState.InCleaning || TargetRoom.State == RoomState.Dirty || TargetRoom.State == RoomState.Emergency)
            {
                _isCleaning = true;
                TargetRoom.State = RoomState.InCleaning;
            }
        }

        public override void Update(float deltaTime)
        {
            GoClean();

            // Check to see if the cleaner is currently cleaning, will be set to true when arriving at a dirty room.
            if (_isCleaning)
            {
                // If the time it takes for the cleaning to finish is over, stop cleaning else reduce the current time.
                if(_cleaningTimer > 0)
                {
                    _cleaningTimer -= deltaTime;
                }
                else
                {
                    // Stop cleaning
                    _isCleaning = false;
                    // Set the room that has been cleaned as clean
                    if (CurrentRoom is GuestRoom)
                    {
                        if ((CurrentRoom as GuestRoom).Guest == null)
                            CurrentRoom.State = RoomState.Vacant;
                        else
                            CurrentRoom.State = RoomState.Occupied;
                    }
                    else
                    {
                        CurrentRoom.State = RoomState.None;
                    }
                    // This cleaner is not busy anymore.
                    _isBusy = false;
                }
            }

            base.Update(deltaTime);
        }

        public void GoClean()
        {
            // Check if this cleaner is busy with cleaning or walking
            if (!_isBusy)
            {
                // Call dijkstra's because there is an emergency.
                FindAndTargetRoom(x => x.State == RoomState.Emergency);

                if (Path != null)
                {
                    // Set the time it takes to clean the room.
                    _cleaningTimer = TargetRoom.GetTimeToCleanEmergency();
                }
                else
                {
                    FindAndTargetRoom(x => x.State == RoomState.Dirty);
                    
                    if(Path != null)
                    {
                        // Set the time it takes to clean the room.
                        _cleaningTimer = TargetRoom.GetTimeToCleanEmergency();
                    }
                    else
                    {
                        return;
                    }
                }

                // Set the Target room to InCleaning to claim it.
                TargetRoom.State = RoomState.InCleaning;
                // Set busy to true because this cleaner is busy with walking or cleaning.
                _isBusy = true;
            }
        }
    }
}

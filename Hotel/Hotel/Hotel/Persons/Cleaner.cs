using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework;
using Hotel.Rooms;

namespace Hotel.Persons
{
    public class Cleaner : Person
    {
        public bool IsBusy { get; private set; }

        private float _cleaningTimer;
        private float _cleaningDuration;
        private bool _isCleaning;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="room">The room to spawn the cleaner in.</param>
        public Cleaner(Room room) : base(room)
        {
            Name = "Cleaner";
            Sprite.LoadSprite("Cleaner");
            _isCleaning = false;

            // Load settings from the config.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();
            _cleaningDuration = (float)config.CleaningDuration;
            _cleaningTimer = _cleaningDuration;
        }

        /// <summary>
        /// Call this to kill the cleaner.
        /// </summary>
        public override void Death()
        {
            // Set an emergency in this room.
            if (CurrentRoom.State != RoomState.Emergency && CurrentRoom.State != RoomState.InCleaning)
                CurrentRoom.SetEmergency(8);

            // Unclaim the room which the cleaner was supposed to clean.
            if (TargetRoom != null && TargetRoom.State == RoomState.InCleaning)
                TargetRoom.State = TargetRoom.PrevRoomState;

            // Call the base.
            base.Death();
        }

        public override void OnArrival()
        {
            base.OnArrival();
  
            // Cleaner has arrived at the dirty room.
            // If the room where the cleaner has arrived at is dirty set _isCleaning to true.
            if(TargetRoom.State == RoomState.InCleaning || TargetRoom.State == RoomState.Dirty || TargetRoom.State == RoomState.Emergency)
            {
                _isCleaning = true;
                TargetRoom.State = RoomState.InCleaning;
            }
        }

        /// <summary>
        /// Tells the cleaner to clean the room that is given.
        /// </summary>
        /// <param name="room">Clean this room.</param>
        public void GoClean(Room room)
        {
            if (Evacuating || IsBusy)
                return;

            if (room.State == RoomState.Emergency || room.State == RoomState.Dirty)
            {
                FindAndTargetRoom(x => x == room);

                if (TargetRoom.State == RoomState.Emergency)
                    _cleaningTimer = TargetRoom.GetTimeToCleanEmergency();
                else
                    _cleaningTimer = _cleaningDuration;

                TargetRoom.State = RoomState.InCleaning;
                IsBusy = true;
            }
        }

        public override void Update(float deltaTime)
        {
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
                        // When an emergency happened we dont want to set the room state to vacant, because there is still someone in that room.
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
                    IsBusy = false;

                    // Check if there is an evacuation, and evacuate if there is.
                    if(Evacuating)
                        FindAndTargetRoom(x => x.Name == "Outside");
                }
            }

            base.Update(deltaTime);
        }
    }
}

using Microsoft.Xna.Framework.Content;
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
        private List<Room> _allRoomsInHotel;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Cleaner(ContentManager content, Room room, float survivability, float walkingSpeed, int cleaningDuration, List<Room> allRooms) : base(content, room, survivability, walkingSpeed)
        {
            Name = "Cleaner";
            Sprite.LoadSprite("Cleaner");
            _isCleaning = false;
            _cleaningDuration = (float)cleaningDuration;
            _allRoomsInHotel = allRooms;
            _cleaningTimer = _cleaningDuration;

            Arrival += Cleaner_Arrival;
        }

        private void Cleaner_Arrival(object sender, EventArgs e)
        {
            // Cleaner has arrived at the dirty room.
            // If the room where the cleaner has arrived at is dirty set _isCleaning to true.
            if(TargetRoom.State == RoomState.InCleaning)
                _isCleaning = true;
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
                    CurrentRoom.State = RoomState.Vacant;
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
                bool thereIsADirtyRoom = false;
                bool thereIsAEmergency = false;

                // Check first if there is a dirty room or a room with an emergency.
                foreach (Room room in _allRoomsInHotel)
                {
                    if(room.State == RoomState.Emergency)
                    {
                        thereIsAEmergency = true;
                    }

                    if (room.State == RoomState.Dirty)
                    {
                        thereIsADirtyRoom = true;
                    }
                }

                // Check if a room with a emergency was found.
                if (thereIsAEmergency)
                {
                    // Call dijkstra's because there is an emergency.
                    //Path = _pathFinder.FindPathToRoomWithState(CurrentRoom, RoomState.Emergency);
                    FindAndTargetRoom(x => x.State == RoomState.Emergency);
                    // Set the time it takes to clean the room.
                    _cleaningTimer = TargetRoom.GetTimeToCleanEmergency();
                }
                else
                {
                    // Check if a room with a dirty state was found.
                    if (thereIsADirtyRoom)
                    {
                        // Call dijkstra's because there is a dirty room.
                        //Path = _pathFinder.FindPathToRoomWithState(CurrentRoom, RoomState.Dirty);
                        FindAndTargetRoom(x => x.State == RoomState.Dirty);

                        // Set teh time it takes to clean the room.
                        _cleaningTimer = _cleaningDuration;
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

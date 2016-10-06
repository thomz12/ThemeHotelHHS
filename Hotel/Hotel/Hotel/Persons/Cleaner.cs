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
        public Cleaner(ContentManager content, Room room, float walkingSpeed, int cleaningDuration, List<Room> allRooms) : base(content, room, walkingSpeed)
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
                    // Reset the timer
                    _cleaningTimer = _cleaningDuration;
                    // Set the room that has been cleaned as clean
                    CurrentRoom.State = RoomState.Vacant;
                    // This cleaner is not busy anymore.
                    _isBusy = false;

                    // Check again if there are dirty rooms
                    GoClean();
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

                // Check first if there is a dirty room.
                foreach (Room room in _allRoomsInHotel)
                {
                    if (room.State == RoomState.Dirty)
                    {
                        thereIsADirtyRoom = true;
                    }
                }

                // There are no dirty rooms so we quit this function before calling Dijkstra's
                if (!thereIsADirtyRoom)
                    return;

                // Call dijkstra's because there is a dirty room.
                PathFinder pathFinder = new PathFinder();
                Path = pathFinder.FindPathToDirtyRoom(CurrentRoom);
                // Set the target room.
                TargetRoom = Path.Last();
                // Set the Target room to InCleaning to claim it.
                TargetRoom.State = RoomState.InCleaning;
                // Set busy to true because this cleaner is busy with walking or cleaning.
                _isBusy = true;
            }
        }
    }
}

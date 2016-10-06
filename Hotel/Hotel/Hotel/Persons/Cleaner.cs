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
        private int _cleaningTimer;
        private int _cleaningDuration;
        private bool _isCleaning;
        private List<Room> _allRoomsInHotel; 

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Cleaner(ContentManager content, Room room, float walkingSpeed, int cleaningDuration, List<Room> allRooms) : base(content, room, walkingSpeed)
        {
            Sprite.LoadSprite("Cleaner");
            _isCleaning = false;
            _cleaningDuration = cleaningDuration;
            _allRoomsInHotel = allRooms;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public void GoClean()
        {
            bool thereIsADirtyRoom = false;

            // Check first if there is a dirty room.
            foreach (Room room in _allRoomsInHotel)
            {
                if(room.State == RoomState.Dirty)
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
        }

        public override string ToString()
        {
            return $"{Name};In Room: {CurrentRoom.Name}{Environment.NewLine}Target: {TargetRoom.Name}{Environment.NewLine}";
        }
    }
}

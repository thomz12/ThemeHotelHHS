using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;

namespace Hotel
{
    public class Hotel
    {
        public List<Room> Rooms { get; set; }

        private ContentManager _contentManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in room images.</param>
        public Hotel(ContentManager content)
        {
            Rooms = new List<Room>();

            PlaceRoom(new GuestRoom(content, new Point(0, 0), new Point(1, 1)));
            PlaceRoom(new GuestRoom(content, new Point(1, 0), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(content, new Point(2, 0)));

            PlaceRoom(new GuestRoom(content, new Point(0, 1), new Point(1, 1)));
            PlaceRoom(new GuestRoom(content, new Point(1, 1), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(content, new Point(2, 1)));

            PlaceRoom(new GuestRoom(content, new Point(0, 2), new Point(1, 1)));
            PlaceRoom(new GuestRoom(content, new Point(1, 2), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(content, new Point(2, 2)));
        }

        public void PlaceRoom(Room room)
        {
            foreach(Room r in Rooms)
            {
                Direction dir = IsNeighbor(room, r);
                if(dir != Direction.None)
                {
                    room.Neighbors[dir] = r;
                    r.Neighbors[ReverseDirection(dir)] = room;
                }
            }

            Rooms.Add(room);
        }
        
        private Direction ReverseDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.None:
                    return Direction.None;
                case Direction.North:
                    return Direction.South;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                default:
                    return Direction.None;
            }
        }

        private Direction IsNeighbor(Room room1, Room room2)
        {
            if (room1.RoomPosition.X == room2.RoomPosition.X - room2.RoomSize.X && room1.RoomPosition.Y == room2.RoomPosition.Y)
            {
                return Direction.East;
            }
            else if(room1.RoomPosition.X == room2.RoomPosition.X + room2.RoomSize.X && room1.RoomPosition.Y == room2.RoomPosition.Y)
            {
                return Direction.West;
            }
            else if(room1.RoomPosition.Y == room2.RoomPosition.Y + room2.RoomSize.Y && room1.RoomPosition.X == room2.RoomPosition.X)
            {
                return Direction.North;
            }
            else if (room1.RoomPosition.Y == room2.RoomPosition.Y - room2.RoomSize.Y && room1.RoomPosition.X == room2.RoomPosition.X)
            {
                return Direction.South;
            }

            return Direction.None;
        }

        public void Update(GameTime gameTime)
        {
            foreach(Room room in Rooms)
            {
                room.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            foreach(Room room in Rooms)
            {
                room.Draw(batch, gameTime);
            }
        }
    }
}

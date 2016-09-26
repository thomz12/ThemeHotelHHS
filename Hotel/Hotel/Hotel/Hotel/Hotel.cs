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

            _contentManager = content;

            //Read a file and build the hotel!
            // TEMP!
            PlaceRoom(new Lobby(_contentManager, new Point(0, 0), new Point(2, 1)));
            PlaceRoom(new ElevatorShaft(_contentManager, new Point(2, 0)));

            PlaceRoom(new GuestRoom(_contentManager, new Point(0, 1), new Point(1, 1)));
            PlaceRoom(new GuestRoom(_contentManager, new Point(1, 1), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(_contentManager, new Point(2, 1)));

            PlaceRoom(new GuestRoom(_contentManager, new Point(0, 2), new Point(1, 1)));
            PlaceRoom(new GuestRoom(_contentManager, new Point(1, 2), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(_contentManager, new Point(2, 2)));

            PlaceRoom(new GuestRoom(_contentManager, new Point(0, 3), new Point(1, 1)));
            PlaceRoom(new GuestRoom(_contentManager, new Point(1, 3), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(_contentManager, new Point(2, 3)));

            PlaceRoom(new GuestRoom(_contentManager, new Point(1, 4), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(_contentManager, new Point(2, 4)));

            PlaceRoom(new GuestRoom(_contentManager, new Point(0, 5), new Point(1, 2)));
            PlaceRoom(new GuestRoom(_contentManager, new Point(1, 5), new Point(1, 1)));
            PlaceRoom(new ElevatorShaft(_contentManager, new Point(2, 5)));

            PlaceRoom(new Cafe(_contentManager, new Point(0, 6), new Point(2, 1)));
            PlaceRoom(new ElevatorShaft(_contentManager, new Point(2, 6)));
            // /TEMP!
        }

        /// <summary>
        /// Places a room in the hotel.
        /// </summary>
        /// <param name="room">The room to add to the hotel.</param>
        public void PlaceRoom(Room room)
        {
            foreach(Room r in Rooms)
            {
                Direction dir = room.IsNeighbor(r);
                if(dir != Direction.None)
                {
                    room.Neighbors[dir] = r;
                    r.Neighbors[r.ReverseDirection(dir)] = room;
                }
            }

            Rooms.Add(room);
        }
       
        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Update(float deltaTime)
        {
            foreach(Room room in Rooms)
            {
                room.Update(deltaTime);
            }
        }

        /// <summary>
        /// Called when drawing to the screen.
        /// </summary>
        /// <param name="batch">The batch to draw to.</param>
        /// <param name="gameTime">the game time.</param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            foreach(Room room in Rooms)
            {
                room.Draw(batch, gameTime);
            }
        }
    }
}

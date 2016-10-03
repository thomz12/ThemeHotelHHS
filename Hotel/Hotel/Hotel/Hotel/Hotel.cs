using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;
using Hotel.Persons;

namespace Hotel
{
    public class Hotel
    {
        public string HotelLayoutFilePath { get; private set; }
        public List<Room> Rooms { get; set; }
        public List<Person> Persons { get; set; }

        private ContentManager _contentManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in room images.</param>
        public Hotel(ContentManager content)
        {
            Rooms = new List<Room>();
            Persons = new List<Person>();

            // Get the filepath of the layout file.
            HotelLayoutFilePath = @"Extra\Hotel2.layout";

            _contentManager = content;

            // read the hotel from a layout file.
            HotelBuilder builder = new HotelBuilder(content);
            List<Room> buildedRooms = builder.BuildHotel(HotelLayoutFilePath);

            // Add the rooms, and connect them.
            Room outside = new Outside(content, new Point(-1, 0));
            Rooms.Add(outside);
            for (int i = 0; i < buildedRooms.Count; i++)
            {
                PlaceRoom(buildedRooms[i]);
            }

            // temp
            Persons.Add(new Receptionist(_contentManager, Rooms[45]));

            
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                Room rm = Rooms[r.Next(0, Rooms.Count)];
                if(rm is ElevatorShaft)
                {
                    i--;
                    continue;
                }
                Persons.Add(new Guest(_contentManager, rm));

                Room tr = Rooms[r.Next(0, Rooms.Count)];
                if (tr is ElevatorShaft)
                {
                    i--;
                    continue;
                }

                Persons.Last().TargetRoom = tr;
            }

            AddGuest();
            // /temp
        }

        public void AddGuest()
        {
            Guest guest = new Guest(_contentManager, Rooms[0]);
            guest.TargetRoom = Rooms[3];
            Persons.Add(guest);
        }

        /// <summary>
        /// Places a room in the hotel. 
        /// </summary>
        /// <param name="room">The room to add to the hotel.</param>
        public void PlaceRoom(Room room)
        {
            foreach (Room r in Rooms)
            {
                Direction dir = room.IsNeighbor(r);
                if (dir != Direction.None)
                {
                    room.Neighbors[dir] = r;
                    r.Neighbors[r.ReverseDirection(dir)] = room;
                }
            }

            Rooms.Add(room);
        }

        /// <summary>
        /// Gets the object on a given point in the world
        /// </summary>
        /// <param name="position">The point the object should contain</param>
        /// <returns>Null if nothing was found, if something was found, return that object.</returns>
        public HotelObject GetObject(Point position)
        {
            foreach (Person person in Persons)
            {
                if (person.BoundingBox.Contains(position))
                    return person;
            }

            foreach (Room room in Rooms)
            {
                if (room.BoundingBox.Contains(position))
                    return room;
            }

            return null;
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Update(float deltaTime)
        {
            foreach (Room room in Rooms)
            {
                room.Update(deltaTime);
            }

            foreach (Person person in Persons)
            {
                person.Update(deltaTime);
            }
        }

        /// <summary>
        /// Called when drawing to the screen.
        /// </summary>
        /// <param name="batch">The batch to draw to.</param>
        /// <param name="gameTime">the game time.</param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            foreach (Person person in Persons)
            {
                person.Draw(batch, gameTime);
            }

            foreach (Room room in Rooms)
            {
                room.Draw(batch, gameTime);
            }
        }
    }
}
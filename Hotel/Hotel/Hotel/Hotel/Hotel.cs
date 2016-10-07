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
        public Dictionary<string, Person> Persons { get; set; }
        public Receptionist Receptionist { get; set; }

        private ContentManager _contentManager;
        private ConfigModel _config;
        private int _cleaners;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in room images.</param>
        public Hotel(ContentManager content, ConfigModel config)
        {
            Rooms = new List<Room>();
            Persons = new Dictionary<string, Person>();

            _config = config;
            _contentManager = content;

            // Set the path to the file of the hotel that needs to be loaded.
            HotelLayoutFilePath = config.LayoutPath;

            // read the hotel from a layout file.
            HotelBuilder builder = new HotelBuilder(content, config);
            List<Room> buildedRooms = builder.BuildHotel(HotelLayoutFilePath);

            // Add the rooms, and connect them, starts with an empty room outside with ID 0.
            Room outside = new EmptyRoom(content, 0, new Point(-1, 0), new Point(1, 1));
            Rooms.Add(outside);
            for (int i = 0; i < buildedRooms.Count; i++)
            {
                PlaceRoom(buildedRooms[i]);
            }

            _cleaners = 2;

            CreateStaff();
        }

        /// <summary>
        ///  Creates the staff for the hotel.
        /// </summary>
        public void CreateStaff()
        {
            Room firstLobby = Rooms.OfType<Lobby>().OrderBy(x => x.RoomPosition.X).First();
            Receptionist = new Receptionist(_contentManager, firstLobby, Rooms, _config.WalkingSpeed);
            Persons.Add("Receptionist", Receptionist);

            for (int i = 0; i < _cleaners; i++)
            {
                Persons.Add("Cleaner" + i, new Cleaner(_contentManager, firstLobby, _config.WalkingSpeed, _config.CleaningDuration, Rooms));
            }
        }

        /// <summary>
        /// Creates a guest outside.
        /// </summary>
        public void AddGuest(string name, int stars)
        {
            Guest guest = new Guest(_contentManager, Rooms[0], _config.WalkingSpeed);
            guest.StayState = StayState.CheckIn;
            guest.Classification = stars;
            Persons.Add(name, guest);
            guest.TargetRoom = Receptionist.CurrentRoom;
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
            foreach (Person person in Persons.Values)
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
            for (int i = 0; i < Rooms.Count; i++)
            {
                Rooms[i].Update(deltaTime);
            }

            for (int i = 0; i < Persons.Count; i++)
            {
                Persons.Values.ElementAt(i).Update(deltaTime);
            }
        }

        /// <summary>
        /// Called when drawing to the screen.
        /// </summary>
        /// <param name="batch">The batch to draw to.</param>
        /// <param name="gameTime">the game time.</param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                Rooms[i].Draw(batch, gameTime);
            }

            for (int i = 0; i < Persons.Count; i++)
            {
                Persons.Values.ElementAt(i).Draw(batch, gameTime);
            }
        }
    }
}
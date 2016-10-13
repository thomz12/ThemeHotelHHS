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
        public List<Person> Staff { get; set; }
        public Dictionary<string, Person> Guests { get; set; }
        public Receptionist Receptionist { get; set; }

        private int _cleaners;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in room images.</param>
        public Hotel()
        {
            Rooms = new List<Room>();
            Staff = new List<Person>();
            Guests = new Dictionary<string, Person>();

            // Set the path to the file of the hotel that needs to be loaded.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();
            HotelLayoutFilePath = config.LayoutPath;

            // read the hotel from a layout file.
            HotelBuilder builder = new HotelBuilder();
            List<Room> buildedRooms = builder.BuildHotel(HotelLayoutFilePath);

            // Add the rooms, and connect them, starts with an empty room outside with ID 0.
            Room outside = new EmptyRoom(0, new Point(-1, 0), new Point(1, 1));
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
            IOrderedEnumerable<Lobby> lobbies = Rooms.OfType<Lobby>().OrderBy(x => x.RoomPosition.X);
            Lobby aLobby = lobbies.ElementAt(lobbies.Count() / 2);
            Receptionist = new Receptionist(aLobby, Rooms);
            Staff.Add(Receptionist);

            Random r = new Random();
            for (int i = 0; i < _cleaners; i++)
            {
                Cleaner c = new Cleaner(Rooms[r.Next(1, Rooms.Count())]);
                Staff.Add(c);
                c.RemoveObject += Remove_Cleaner;
            }
        }

        /// <summary>
        /// Creates a guest outside.
        /// </summary>
        public void AddGuest(string name, int stars)
        {
            Guest guest = new Guest(Rooms[0]);
            guest.StayState = StayState.CheckIn;
            guest.Classification = stars;
            Guests.Add(name, guest);
            guest.FindAndTargetRoom(x => (x is Lobby && (x as Lobby).Receptionist != null));
            
            // Subscribe to remove event
            guest.RemoveObject += Guest_RemoveObject;
        }

        private void Guest_RemoveObject(object sender, EventArgs e)
        {
            RemoveObject((HotelObject)sender);
        }

        private void CGhost_RemoveObject(object sender, EventArgs e)
        {
            RemoveObject((HotelObject)sender);
        }

        private void Remove_Cleaner(object sender, EventArgs e)
        {
            RemoveObject((HotelObject)sender);
        }

        /// <summary>
        /// Remove a object.
        /// </summary>
        /// <param name="object">The object to remove.</param>
        public void RemoveObject(HotelObject hotelObject)
        {
            if (hotelObject is Person)
            {
                // Remove guest from the hotel.
                if (hotelObject is Guest)
                {
                    string item = Guests.First(x => x.Value == hotelObject).Key;
                    Guest guest = (Guest)hotelObject;

                    if (guest.Room != null)
                    {
                        if (guest.Room.State != RoomState.Emergency && guest.Room.State != RoomState.InCleaning)
                            guest.Room.State = RoomState.Dirty;
                        guest.Room = null;
                    }

                    //Remove the guest from a checkin/out queue where applicable.
                    if (guest.CurrentRoom is Lobby)
                    {
                        (guest.CurrentRoom as Lobby).RemoveFromQueues(guest);
                    }

                    Guests.Remove(item);
                }
                // Remove cleaner from the hotel.
                else if (hotelObject is Cleaner)
                {
                    Cleaner cleaner = (Cleaner)hotelObject;
                    Staff.Remove(cleaner);

                    // Spawn a ghost
                    CleanerGhost cGhost = new CleanerGhost(cleaner.CurrentRoom);
                    cGhost.RemoveObject += CGhost_RemoveObject;
                    Staff.Add(cGhost);
                }
                else if (hotelObject is CleanerGhost)
                {
                    CleanerGhost cleanerGhost = (CleanerGhost)hotelObject;

                    // Spawn a new cleaner
                    Cleaner cleaner = new Cleaner(Rooms[0]);
                    cleaner.RemoveObject += Remove_Cleaner;
                    Staff.Add(cleaner);
                    // Make it walk indoors
                    cleaner.FindAndTargetRoom(x => (x is Lobby && (x as Lobby).Receptionist != null));

                    Staff.Remove(cleanerGhost);
                }

                hotelObject.RemoveObject -= Guest_RemoveObject;
            }
            if (hotelObject is Room)
            {
                throw new NotImplementedException();
            }
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

            // Subscribe to the remove event
            room.RemoveObject += Room_RemoveObject;

            Rooms.Add(room);
        }

        private void Room_RemoveObject(object sender, EventArgs e)
        {
            RemoveObject((HotelObject)sender);
        }

        /// <summary>
        /// Gets the object on a given point in the world
        /// </summary>
        /// <param name="position">The point the object should contain</param>
        /// <returns>Null if nothing was found, if something was found, return that object.</returns>
        public HotelObject GetObject(Point position)
        {
            foreach (Person person in Guests.Values)
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

            for (int i = 0; i < Staff.Count; i++)
            {
                Staff[i].Update(deltaTime);
            }

            for (int i = 0; i < Guests.Count; i++)
            {
                Guests.Values.ElementAt(i).Update(deltaTime);
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

            for (int i = 0; i < Staff.Count; i++)
            {
                Staff[i].Draw(batch, gameTime);
            }

            for (int i = 0; i < Guests.Count; i++)
            {
                Guests.Values.ElementAt(i).Draw(batch, gameTime);
            }
        }
    }
}
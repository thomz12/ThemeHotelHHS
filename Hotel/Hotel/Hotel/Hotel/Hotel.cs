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
        public HotelBuilder HotelBuilder { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Person> Staff { get; set; }
        public Dictionary<string, Person> Guests { get; set; }

        public bool Evacuating;

        private int _cleaners;

        /// <summary>
        /// Constructor
        /// </summary>
        public Hotel()
        {
            Staff = new List<Person>();
            Guests = new Dictionary<string, Person>();

            Rooms = new List<Room>();
            HotelBuilder = new HotelBuilder();

            Evacuating = false;
        }

        /// <summary>
        /// Call this to build the hotel.
        /// Only call this after the room builder components have been registered.
        /// </summary>
        public void BuildHotel()
        {
            // Build the hotel.
            Rooms = HotelBuilder.BuildHotel(); 

            // Create the staff for the hotel.
            _cleaners = ServiceLocator.Get<ConfigLoader>().GetConfig().NumberOfCleaners;

            CreateStaff();
        }

        /// <summary>
        /// Creates the staff for the hotel.
        /// </summary>
        public void CreateStaff()
        {
            IOrderedEnumerable<Lobby> lobbies = Rooms.OfType<Lobby>().OrderBy(x => x.RoomPosition.X);
            Lobby aLobby = lobbies.ElementAt(lobbies.Count() / 2);
            Receptionist receptionist = new Receptionist(aLobby, Rooms);
            receptionist.RemoveObjectEvent += RemoveObject;
            Staff.Add(receptionist);

            Random r = new Random();
            for (int i = 0; i < _cleaners; i++)
            {
                Cleaner c = new Cleaner(lobbies.ElementAt(0));
                Staff.Add(c);
                c.RemoveObjectEvent += RemoveObject;
            }
        }

        /// <summary>
        /// Creates a guest outside.
        /// </summary>
        public void AddGuest(string name, int stars)
        {
            Guest guest = new Guest(Rooms.Where(x => x.Name.Equals("Outside")).FirstOrDefault());
            guest.StayState = StayState.CheckIn;
            guest.Classification = stars;
            Guests.Add(name, guest);
            guest.FindAndTargetRoom(x => (x is Lobby && (x as Lobby).Receptionist != null));

            // Subscribe to remove event
            guest.RemoveObjectEvent += RemoveObject;
        }

        /// <summary>
        /// Remove a object.
        /// </summary>
        /// <param name="object">The object to remove.</param>
        public void RemoveObject(object sender, EventArgs e)
        {
            HotelObject hotelObject = (HotelObject)sender;

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
                // Remove receptionist from hotel.
                else if(hotelObject is Receptionist)
                {
                    Lobby lobby = Rooms.Where(x => x is Lobby && (x as Lobby).Receptionist == hotelObject) as Lobby;
                    if(lobby != null)
                        lobby.Receptionist = null;
                    Staff.Remove(hotelObject as Person);
                }
                // Remove cleaner from the hotel.
                else if (hotelObject is Cleaner)
                {
                    Cleaner cleaner = (Cleaner)hotelObject;
                    Staff.Remove(cleaner);

                    // Spawn a ghost
                    CleanerGhost cGhost = new CleanerGhost(cleaner.CurrentRoom);
                    cGhost.RemoveObjectEvent += RemoveObject;
                    Staff.Add(cGhost);
                }
                else if (hotelObject is CleanerGhost)
                {
                    CleanerGhost cleanerGhost = (CleanerGhost)hotelObject;

                    // Spawn a new cleaner
                    Cleaner cleaner = new Cleaner(Rooms[0]);
                    cleaner.RemoveObjectEvent += RemoveObject;
                    Staff.Add(cleaner);
                    // Make it walk indoors
                    cleaner.FindAndTargetRoom(x => (x is Lobby && (x as Lobby).Receptionist != null));

                    Staff.Remove(cleanerGhost);
                }

                hotelObject.RemoveObjectEvent -= RemoveObject;
            }
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

            foreach (Person person in Staff)
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
        /// Gets the closest cleaner to each room in the given list and sends them to that room.
        /// </summary>
        /// <param name="dirtyRooms">A List with dirty rooms.</param>
        private void SendCleanerToRoom(List<Room> dirtyRooms)
        {
            // Create a new pathfinder.
            PathFinder pf = new PathFinder();

            // Calculate for each room in a collection the distance that every cleaner is away from that room and save it in a dictionary.
            foreach (Room room in dirtyRooms)
            {
                // All lengths from each cleaner to this room.
                Dictionary<Cleaner, int> lengths = new Dictionary<Cleaner, int>();

                // Cycle through all the staff.
                for (int i = 0; i < Staff.Count; i++)
                {
                    // Check if this staff is a cleaner.
                    if (Staff[i] is Cleaner)
                    {
                        // Cast.
                        Cleaner cl = (Cleaner)Staff[i];

                        // Check if this cleaner is busy.
                        if (!cl.IsBusy)
                        {
                            // Find a new path to the room that we are working with.
                            List<Room> aPath = pf.Find(cl.CurrentRoom, x => x == room);

                            // No path was found, continue.
                            if (aPath == null)
                                continue;

                            // Normalize the length (2 long rooms are counted as 2 then)
                            int length = 0;
                            foreach (Room r in aPath)
                            {
                                length += r.RoomSize.X;
                            }

                            // Add this cleaner and the distance it needs to travel to a dictionary.
                            lengths.Add(cl, length);
                        }
                    }
                }

                // Check if there is someone in the dictionary.
                if (lengths.Count > 0)
                {
                    // Get the cleaner with the shortest path to the room with an emergency
                    Cleaner c = lengths.OrderBy(kvp => kvp.Value).First().Key;
                    // Send the cleaner to clean the room
                    c.GoClean(room);
                }
            }
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Update(float deltaTime)
        {
            if(Evacuating)
            {
                Evacuation();
            }

            List<Room> er = new List<Room>();
            List<Room> dr = new List<Room>();

            // Update all the rooms and sends a cleaner to the dirty ones
            for (int i = 0; i < Rooms.Count; i++)
            {
                Rooms[i].Update(deltaTime);

                if (Rooms[i].State == RoomState.Emergency)
                    er.Add(Rooms[i]);
                else if (Rooms[i].State == RoomState.Dirty)
                    dr.Add(Rooms[i]);
            }

            // First send all the cleaners to clean emergencies.
            SendCleanerToRoom(er);
            // Then send all the cleaners to clean normal dirty rooms.
            SendCleanerToRoom(dr);

            // Update all the staff
            for (int i = 0; i < Staff.Count; i++)
            {
                Staff[i].Update(deltaTime);
            }

            // Update all the guests
            for (int i = 0; i < Guests.Count; i++)
            {
                Guests.Values.ElementAt(i).Update(deltaTime);
            }
        }

        /// <summary>
        /// Called to check for people evacutaion, also ends the evacuation.
        /// </summary>
        public void Evacuation()
        {
            if (Guests.Where(x => x.Value.CurrentRoom.Name == "Outside").Count() == Guests.Count)
            {
                Evacuating = false;

                // Check for all receptionists.
                for (int i = 0; i < Staff.Count; i++)
                {
                    Staff[i].Evacuating = false;

                    if(Staff[i] is Receptionist)
                        Staff[i].FindAndTargetRoom(x => x is Lobby && (x as Lobby).Receptionist == Staff[i]);
                }

                for (int i = 0; i < Guests.Count; i++)
                {
                    Guests.ElementAt(i).Value.Evacuating = false;
                    Guests.ElementAt(i).Value.FindAndTargetRoom(x => (Guests.ElementAt(i).Value as Guest).Room == x);
                }
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
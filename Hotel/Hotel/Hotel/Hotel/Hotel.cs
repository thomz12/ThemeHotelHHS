﻿using Microsoft.Xna.Framework;
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
            Staff = new List<Person>();
            Guests = new Dictionary<string, Person>();

            // read the hotel from a layout file.
            HotelBuilder builder = new HotelBuilder();
            builder.BuildHotel(ServiceLocator.Get<ConfigLoader>().GetConfig().LayoutPath);

            Rooms = builder.Rooms;

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
            Receptionist = new Receptionist(aLobby, Rooms);
            Staff.Add(Receptionist);

            Random r = new Random();
            for (int i = 0; i < _cleaners; i++)
            {
                Cleaner c = new Cleaner(Rooms[r.Next(1, Rooms.Count())]);
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

        private void SendCleanerToRoom(List<Room> dirtyRooms)
        {
            PathFinder pf = new PathFinder();

            // Calculate for each room in a collection the distance that every cleaner is away from that room and save it in a dictionary.
            foreach (Room room in dirtyRooms)
            {
                // All lengths from each cleaner to this room.
                Dictionary<Cleaner, int> lengths = new Dictionary<Cleaner, int>();

                for (int i = 0; i < Staff.Count; i++)
                {
                    if (Staff[i] is Cleaner)
                    {
                        Cleaner cl = (Cleaner)Staff[i];
                        if (!cl.IsBusy)
                        {
                            List<Room> aPath = pf.Find(cl.CurrentRoom, x => x == room);
                            int length = 0;
                            foreach (Room r in aPath)
                            {
                                length += r.RoomSize.X;
                            }
                            lengths.Add(cl, length);
                        }
                    }
                }
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

            SendCleanerToRoom(er);
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
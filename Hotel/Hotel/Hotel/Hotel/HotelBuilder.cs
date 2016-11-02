using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Hotel.Rooms;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Hotel.RoomsFactory;

namespace Hotel
{
    public class HotelBuilder
    {
        private bool _createEmptyRooms;

        public List<Room> Rooms { get; private set; }
        public RoomFactory RoomFactory { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HotelBuilder()
        {
            _createEmptyRooms = true;
            Rooms = new List<Room>();
            RoomFactory = new RoomFactory();
        }

        /// <summary>
        /// Call this to build the hotel from a file.
        /// </summary>
        public List<Room> BuildHotel()
        {
            // List of all rooms.
            List<Room> rooms = new List<Room>();

            // The hotel width, and height.
            int extremeX = 0;
            int smallestX = Int32.MaxValue;

            int extremeY = 0;
            int smallestY = Int32.MaxValue;

            int extremeID = 0;

            TextReader textReader = new StreamReader(ServiceLocator.Get<ConfigLoader>().GetConfig().LayoutPath);
            JsonReader jsonReader = new JsonTextReader(textReader);

            // Dictionary containg room data.
            Dictionary<string, string> data = new Dictionary<string, string>();

            while (jsonReader.Read())
            {
                Console.WriteLine(jsonReader.Value?.ToString());
                if (jsonReader.Value != null)
                {
                    // Read and add the values from the file.
                    string value = jsonReader.Value.ToString();
                    jsonReader.Read();
                    data.Add(value, jsonReader.Value.ToString());
                }

                // When we know the dimension, add the room.
                if (data.Keys.Count > 0 && jsonReader.Value == null)
                {
                    // if we have data.
                    if (data.Count > 0)
                    {
                        // Get the position/dimension/ID
                        string[] posData = data["Position"].Split(',');
                        string[] dimData = data["Dimension"].Split(',');
                        string[] idData = data["ID"].Split(',');

                        // get the room dimensions.
                        Point dimensions = new Point(Int32.Parse(dimData[0]), Int32.Parse(dimData[1]));
                        // calculate the room position.
                        Point position = new Point(Int32.Parse(posData[0]), Int32.Parse(posData[1]) + (dimensions.Y - 1));
                        // Calculate the ID
                        int id = Int32.Parse(idData[0]);

                        // Set the extreme values (hotel size)
                        int maxX = position.X + (dimensions.X - 1);

                        // Set extreme values these values are used to determine the placement of the lobby, elevator and stairs.
                        if (extremeX < maxX)
                            extremeX = maxX;
                        if (smallestX > position.X)
                            smallestX = position.X;

                        if (extremeY < position.Y)
                            extremeY = position.Y;
                        else if (smallestY > position.Y)
                            smallestY = position.Y;

                        if (extremeID < id)
                            extremeID = id;

                        Room aRoom = RoomFactory.BuildRoom(data);
                        if (aRoom != null)
                            rooms.Add(aRoom);

                        // The option to create empty rooms is on, these empty rooms fill the space in the 2 high rooms so people can "fly" through them.
                        if (_createEmptyRooms)
                        {
                            if (rooms.Count > 0)
                            {
                                // Add empty rooms to rooms bigger in height. (used for path finding)
                                for (int i = 0; i < dimensions.Y - 1; i++)
                                {
                                    rooms.Add(RoomFactory.BuildRoom(-1, "Empty", new Point(position.X, position.Y + i), new Point(dimensions.X, 1)));
                                    rooms.Last().Name = "Empty";
                                }
                            }
                        }
                    }
                    data.Clear();
                }
            }

            // TODO: factory buidling elevatorshaft, lobby, emptyroom.

            // Add elevator shafts to the hotel.
            for (int i = 0; i <= extremeY; i++)
            {
                extremeID++;
                rooms.Add(new ElevatorShaft(extremeID, new Point(smallestX - 1, smallestY - 1 + i)));
            }

            // Add Stairs to the hotel.
            for (int i = 0; i <= extremeY; i++)
            {
                extremeID++;
                rooms.Add(new Staircase(extremeID, new Point(extremeX + 1, smallestY - 1 + i)));
            }

            // Add lobbies.
            for (int i = smallestX; i <= extremeX; i++)
            {
                extremeID++;
                rooms.Add(new Lobby(extremeID, new Point(i, smallestY - 1), new Point(1, 1)));
            }

            // Add the rooms, and connect them, starts with an empty room outside with ID 0.
            EmptyRoom outside = RoomFactory.BuildRoom(0, "Empty", new Point(smallestX - 2, smallestY - 1), new Point(1, 1)) as EmptyRoom;
            outside.Entrance = true;
            rooms.Add(outside);
           
            foreach (Room r in rooms)
                PlaceRoom(r);

            return Rooms;
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
    }
}
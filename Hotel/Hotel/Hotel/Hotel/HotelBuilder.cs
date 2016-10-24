using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Hotel
{
    public class HotelBuilder
    {
        private bool _createEmptyRooms;

        public List<Room> Rooms { get; private set; }

        public HotelBuilder()
        {
            _createEmptyRooms = true;
            Rooms = new List<Room>();
        }

        public void BuildHotel(string path)
        {
            // List of all rooms.
            List<Room> rooms = new List<Room>();

            // The hotel width, and height.
            int extremeX = 0;
            int extremeY = 0;
            int extremeID = 0;

            TextReader textReader = new StreamReader(path);
            JsonReader jsonReader = new JsonTextReader(textReader);

            // Dictionary containg room data.
            Dictionary<string, string> data = new Dictionary<string, string>();

            while(jsonReader.Read())
            {
                Console.WriteLine(jsonReader.Value?.ToString());
                if(jsonReader.Value != null)
                {
                    // Read and add the values from the file.
                    string value = jsonReader.Value.ToString();
                    jsonReader.Read();
                    data.Add(value, jsonReader.Value.ToString());
                }

                // When we know the dimension, add the room.
                if(data.Keys.Count > 0 && jsonReader.Value == null)
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
                        if (extremeX < position.X)
                            extremeX = position.X;
                        if (extremeY < position.Y)
                            extremeY = position.Y;
                        if (extremeID < id)
                            extremeID = id;

                        // All the types of rooms.
                        switch (data["AreaType"])
                        {
                            case "Restaurant":
                                rooms.Add(new Cafe(id, position, dimensions, Int32.Parse(data["Capacity"])));
                                break;
                            case "Room":
                                rooms.Add(new GuestRoom(id, position, dimensions, Int32.Parse(data["Classification"][0].ToString())));
                                break;
                            case "Cinema":
                                rooms.Add(new Cinema(id, position, dimensions));
                                break;
                            case "Fitness":
                                rooms.Add(new Fitness(id, position, dimensions));
                                break;
                            default:
                                break;
                        }

                        if (_createEmptyRooms)
                        {
                            // Add empty rooms to rooms bigger in height. (used for path finding)
                            for (int i = 0; i < dimensions.Y - 1; i++)
                            {
                                Room roomToAddTo = rooms.Last();
                                rooms.Add(new EmptyRoom(-1, new Point(position.X, position.Y + i), new Point(dimensions.X, 1)));
                                rooms.Last().Name = roomToAddTo.Name;
                            }
                        }
                    }
                    data.Clear();
                }
            }

            // Add elevator shafts to the hotel.
            for(int i = 0; i <= extremeY; i++)
            {
                extremeID++;
                rooms.Add(new ElevatorShaft(extremeID, new Point(0, i)));
            }

            // Add Stairs to the hotel.
            for (int i = 0; i <= extremeY; i++)
            {
                extremeID++;
                rooms.Add(new Staircase(extremeID, new Point(extremeX + 1, i)));
            }

            // Add lobbies.
            for(int i = 1; i <= extremeX; i++)
            {
                extremeID++;
                rooms.Add(new Lobby(extremeID, new Point(i, 0), new Point(1,1)));
            }

            foreach (Room r in rooms)
                PlaceRoom(r);
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

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
        private ContentManager _content;

        public HotelBuilder(ContentManager content)
        {
            _content = content;
        }

        public List<Room> BuildHotel(string path)
        {
            // List of all rooms.
            List<Room> rooms = new List<Room>();

            // The hotel width, and height.
            int extremeX = 0;
            int extremeY = 0;

            TextReader textReader = new StreamReader(path);
            JsonReader jsonReader = new JsonTextReader(textReader);

            // Dictionary containg room data.
            Dictionary<string, string> data = new Dictionary<string, string>();

            while(jsonReader.Read())
            {
                if(jsonReader.Value != null)
                {
                    // Read and add the values from the file.
                    string value = jsonReader.Value.ToString();
                    jsonReader.Read();
                    data.Add(value, jsonReader.Value.ToString());
                }

                // When we know the dimension, add the room.
                if(data.ContainsKey("Dimension"))
                {
                    // if we have data.
                    if (data.Count > 0)
                    {
                        // Get the position/dimension.
                        string[] posData = data["Position"].Split(',');
                        string[] dimData = data["Dimension"].Split(',');

                        // get the room dimensions.
                        Point dimensions = new Point(Int32.Parse(dimData[0]), Int32.Parse(dimData[1]));
                        // calculate the room position.
                        Point position = new Point(Int32.Parse(posData[0]), Int32.Parse(posData[1]) + (dimensions.Y - 1));

                        // Set the extreme values (hotel size)
                        if (extremeX < position.X)
                            extremeX = position.X;
                        if (extremeY < position.Y)
                            extremeY = position.Y;

                        // All the types of rooms.
                        switch (data["AreaType"])
                        {
                            case "Restaurant":
                                rooms.Add(new Cafe(_content, position, dimensions, Int32.Parse(data["Capacity"])));
                                break;
                            case "Room":
                                rooms.Add(new GuestRoom(_content, position, dimensions, Int32.Parse(data["Classification"][0].ToString())));
                                break;
                            case "Cinema":
                                rooms.Add(new Cinema(_content, position, dimensions));
                                break;
                            case "Fitness":
                                rooms.Add(new Fitness(_content, position, dimensions));
                                break;
                            default:
                                break;
                        }

                        for (int i = 0; i < dimensions.Y - 1; i++)
                        {
                            Room roomToAddTo = rooms.Last();
                            rooms.Add(new EmptyRoom(_content, new Point(position.X, position.Y + i), new Point(dimensions.X, 1)));
                            rooms.Last().Name = roomToAddTo.Name;
                        }
                    }
                    data.Clear();
                }
            }

            // Add elevator shafts to the hotel.
            for(int i = 0; i <= extremeY; i++)
            {
                rooms.Add(new ElevatorShaft(_content, new Point(0, i)));
            }

            // Add Stairs to the hotel.
            for (int i = 0; i <= extremeY; i++)
            {
                rooms.Add(new Staircase(_content, new Point(extremeX + 1, i)));
            }

            // Add lobbies.
            for(int i = 1; i <= extremeX; i++)
            {
                rooms.Add(new Lobby(_content, new Point(i, 0), new Point(1,1)));
            }

            return rooms;
        }
    }
}

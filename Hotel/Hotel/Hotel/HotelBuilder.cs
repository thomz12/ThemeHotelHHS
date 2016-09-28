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

        public List<Room> Hotel(string path)
        {
            List<Room> rooms = new List<Room>();

            int extremeX = 0;
            int extremeY = 0;

            TextReader textReader = new StreamReader(path);
            JsonReader jsonReader = new JsonTextReader(textReader);

            Dictionary<string, string> data = new Dictionary<string, string>();

            while(jsonReader.Read())
            {
                if(jsonReader.Value != null)
                {
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

                        Point dimensions = new Point(Int32.Parse(dimData[0]), Int32.Parse(dimData[1]));

                        Point position = new Point(Int32.Parse(posData[0]), Int32.Parse(posData[1]) + (dimensions.Y - 1));

                        if (extremeX < position.X)
                            extremeX = position.X;
                        if (extremeY < position.Y)
                            extremeY = position.Y;

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
                    }
                    data.Clear();
                }
            }

            for(int i = 0; i <= extremeY; i++)
            {
                rooms.Add(new ElevatorShaft(_content, new Point(0, i)));
            }

            for (int i = 0; i <= extremeY; i++)
            {
                rooms.Add(new Staircase(_content, new Point(extremeX + 1, i)));
            }

            for(int i = 1; i <= extremeX; i++)
            {
                rooms.Add(new Lobby(_content, new Point(i, 0), new Point(1,1)));
            }

            return rooms;
        }
    }
}

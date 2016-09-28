using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;
using Newtonsoft.Json;

namespace Hotel
{
    public class HotelBuilder
    {
        public HotelBuilder()
        {

        }

        public List<Room> Hotel(string path)
        {
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

                if(data.ContainsKey("Dimension"))
                {
                    data.Clear();
                }
            }

            return null;
        }
    }
}

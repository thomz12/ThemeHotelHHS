using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.RoomsFactory
{
    public class StaircaseFactoryComponent : IRoomFactoryComponent
    {
        public Room BuildRoom(Dictionary<string, string> data)
        {
            // Get the position/dimension/ID
            string[] posData = data["Position"].Split(',');
            string[] idData = data["ID"].Split(',');

            // calculate the room position.
            Point position = new Point(Int32.Parse(posData[0]), Int32.Parse(posData[1]));
            // Calculate the ID
            int id = Int32.Parse(idData[0]);

            return new Rooms.Staircase(id, position);
        }
    }
}

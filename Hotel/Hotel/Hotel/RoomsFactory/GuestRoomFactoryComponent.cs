using Hotel.Rooms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Hotel.RoomsFactory
{
    public class GuestRoomFactoryComponent : IRoomFactoryComponent
    {
        public Room BuildRoom(Dictionary<string, string> data)
        {
            // Get the position/dimension/ID
            string[] posData = data["Position"].Split(',');
            string[] dimData = data["Dimension"].Split(',');
            string[] idData = data["ID"].Split(',');
            string classData = data["Classification"].Split(' ')[0];

            // get the room dimensions.
            Point dimensions = new Point(Int32.Parse(dimData[0]), Int32.Parse(dimData[1]));
            // calculate the room position.
            Point position = new Point(Int32.Parse(posData[0]), Int32.Parse(posData[1]) + (dimensions.Y - 1));
            // Calculate the ID
            int id = Int32.Parse(idData[0]);
            // Calculate the amount of stars of the room.
            int stars = Int32.Parse(classData);

            return new GuestRoom(id, position, dimensions, stars);
        }
    }
}

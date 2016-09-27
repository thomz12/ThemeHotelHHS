using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class RoomNode
    {
        public int Weight { get; set; }
        public Room Room { get; private set; }
        public Room PrevRoom { get; set; }

        public RoomNode(Room room)
        {
            Room = room;
        }
    }
}

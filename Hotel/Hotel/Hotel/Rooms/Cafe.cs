using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Hotel.RoomBehaviours;
using Microsoft.Xna.Framework;

namespace Hotel.Rooms
{
    public class Cafe : Room
    {
        public int Capacity { get; private set; }

        public Cafe(int id, Point position, Point size, int capacity) : base(id, position, size)
        {
            Sprite.LoadSprite("2x1Cafe");
            Capacity = capacity;
            Name = "Restaurant";

            RoomBehaviour = new CafeBehaviour();
        }
    }
}

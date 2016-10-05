using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Hotel.Rooms
{
    public class Cafe : Room
    {
        public int Capacity { get; private set; }

        public Cafe(ContentManager content, int id, Point position, Point size, int capacity) : base(content, id, position, size)
        {
            Sprite.LoadSprite("2x1Cafe");
            Capacity = capacity;
            Name = "Cafe";
            Weight = 1000;
        }
    }
}

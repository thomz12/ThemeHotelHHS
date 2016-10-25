using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Hotel.Rooms
{
    public class Pool : Room
    {
        public Pool(int id, Point position, Point size) : base(id, position, size)
        {
            Sprite.LoadSprite("3x1Pool");
            Name = "Pool";
        }
    }
}

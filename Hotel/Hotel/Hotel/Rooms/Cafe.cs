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
        public Cafe(ContentManager content, Point position, Point size) : base(content, position, size)
        {
            Sprite.LoadSprite("2x1Cafe");
            Name = "Cafe";
        }
    }
}

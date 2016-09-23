using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework.Content;

namespace Hotel.Rooms
{
    public class Cafe : Room
    {
        public Cafe(ContentManager content) : base(content)
        {
            Sprite.LoadSprite("2x1Cafe");
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class GuestRoom : Room
    {
        public GuestRoom(ContentManager content) : base(content)
        {
            Sprite.LoadSprite("img");
            Sprite.Color = Color.Blue;
        }
    }
}

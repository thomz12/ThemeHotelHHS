using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    public class Lobby : Room
    {
        public Lobby(ContentManager content, Point position, Point size) : base(content, position, size)
        {
            Sprite.LoadSprite("2x1Lobby");
        }
    }
}

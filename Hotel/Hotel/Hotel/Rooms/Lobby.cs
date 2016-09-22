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
        public Lobby(ContentManager content) : base(content)
        {
            Sprite.LoadSprite("img");
            Sprite.Color = Color.Red;
        }
    }
}

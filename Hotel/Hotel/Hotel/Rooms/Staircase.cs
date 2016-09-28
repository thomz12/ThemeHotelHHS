using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    class Staircase : Room
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager.</param>
        /// <param name="position">The position of the room.</param>
        /// <param name="size">the size of the room.</param>
        public Staircase(ContentManager content, Point position) : base(content, position, new Point(1,1))
        {
            Sprite.LoadSprite("Stairs");
            Vertical = true;
            Weight = 10;
        }
    }
}

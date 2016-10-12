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
        public Staircase(int id, Point position, float weight) : base(id, position, new Point(1,1))
        {
            Sprite.LoadSprite("Stairs");
            Name = "Staircase";
            Vertical = true;
            Weight = weight;
        }
    }
}

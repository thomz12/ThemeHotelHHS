using Microsoft.Xna.Framework;
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
        public Staircase(int id, Point position) : base(id, position, new Point(1,1))
        {
            Sprite.LoadSprite("Stairs");
            Name = "Staircase";
            Vertical = true;

            // Load settings from the config.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();
            Weight = config.StaircaseWeight;
        }
    }
}

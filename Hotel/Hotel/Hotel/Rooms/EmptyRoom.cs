using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    public class EmptyRoom : Room
    {
        public bool Entrance { get; set; }

        /// <summary>
        /// An empty room which cant do anything.
        /// </summary>
        /// <param name="ID">The id given to the room.</param>
        /// <param name="position">The position of the room.</param>
        /// <param name="size">The size of the room.</param>
        public EmptyRoom(int id, Point position, Point size) : base(id, position, size)
        {
            BoundingBox = new Rectangle();
            Name = "Outside";
        }

        // Wont be calling the base update.
        public override void Update(float deltaTime)
        {
            // Lol do nothing.
        }
    }
}

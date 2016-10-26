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
        public EmptyRoom(int id, Point position, Point size) : base(id, position, size)
        {
            BoundingBox = new Rectangle();
            Name = "Outside";
        }

        // Wont be calling the base update.
        public override void Update(float deltaTime)
        {
        }
    }
}

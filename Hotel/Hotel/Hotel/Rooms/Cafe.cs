using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework;

namespace Hotel.Rooms
{
    public class Cafe : Room
    {
        public int Capacity { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ID">The id given to the room.</param>
        /// <param name="position">The position of the room.</param>
        /// <param name="size">The size of the room.</param>
        /// <param name="capacity">The amount of people that can stay in this room at the same time.</param>
        public Cafe(int id, Point position, Point size, int capacity) : base(id, position, size)
        {
            Sprite.LoadSprite("2x1Cafe");
            Capacity = capacity;
            Name = "Restaurant";

            // Make a new behaviour.
            RoomBehaviour = new CafeBehaviour();
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    public class Fitness : Room
    {
        public Fitness(int id, Point position, Point size) : base(id, position, size)
        {
            Sprite.LoadSprite("3x1Fitness");
            Name = "Fitness";
            RoomBehaviour = new FitnessBehaviour();
        }
    }
}

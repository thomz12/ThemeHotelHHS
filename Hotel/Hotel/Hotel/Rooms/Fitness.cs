using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    public class Fitness : Room
    {
        public Fitness(ContentManager content, int id, Point position, Point size) : base(content, id, position, size)
        {
            Sprite.LoadSprite("3x1Fitness");
            Name = "Fitness";
        }
    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    class EmptyRoom : Room
    {
        public EmptyRoom(ContentManager content, int id, Point position, Point size) : base(content, id, position, size)
        {
            BoundingBox = new Rectangle();
            Name = "Outside";
        }

        public override void Update(float deltaTime)
        {
        }
    }
}

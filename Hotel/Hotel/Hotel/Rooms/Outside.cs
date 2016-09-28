using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    class Outside : Room
    {
        public Outside(ContentManager content, Point position) : base(content, position, new Point(1, 1))
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hotel
{
    public class ElevatorShaft : Room
    {
        private Elevator _elevator;

        public int floor;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in images</param>
        public ElevatorShaft(ContentManager content) : base(content)
        {
            
        }
    }
}

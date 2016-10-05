using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Hotel.Persons
{
    class Receptionist : Person
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Receptionist(ContentManager content, Room room, float walkingSpeed) : base(content, room, walkingSpeed)
        {
            Name = "Receptionist";
            Sprite.LoadSprite("Receptionist");
        }
    }
}

using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework;

namespace Hotel.Persons
{
    public class Cleaner : Person
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Cleaner(ContentManager content, Room room, float walkingSpeed) : base(content, room, walkingSpeed)
        {
            Sprite.LoadSprite("Cleaner");
        }

        public override string ToString()
        {
            return $"{Name};In Room: {CurrentRoom.Name}{Environment.NewLine}Target: {TargetRoom.Name}{Environment.NewLine}";
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;

namespace Hotel.Persons
{
    public abstract class Person : GameObject
    {
        public float WalkingSpeed { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Person(ContentManager content) : base(content)
        {

        }

        public void FindPath()
        {

        }
    }
}

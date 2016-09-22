using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;

namespace Hotel.Persons
{
    public class Cleaner : Person
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Cleaner(ContentManager content) : base(content)
        {

        }
    }
}

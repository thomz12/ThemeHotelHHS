using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework.Content;

namespace Hotel.Persons
{
    class Receptionist : Person
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Receptionist(ContentManager content) : base(content)
        {

        }
    }
}

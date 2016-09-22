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
    public abstract class Person
    {
        public Vector2 Position { get; set; }

        public Sprite Sprite { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Person(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(Position, gameTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);
        }

        public void FindPath()
        {

        }
    }
}

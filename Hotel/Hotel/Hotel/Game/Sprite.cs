using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        private ContentManager _content;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">The path to the file to load</param>
        /// <param name="content">The content manager used to load in images</param>
        public Sprite(ContentManager content)
        {
            _content = content;
            Color = Color.White;
        }

        public void LoadSprite(string path)
        {
            Texture = _content.Load<Texture2D>(path);
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(Vector2 position, GameTime gameTime)
        {
            Position = position;
        }

        /// <summary>
        /// Called when drawing to the screen.
        /// </summary>
        /// <param name="batch">The batch to draw to.</param>
        /// <param name="gameTime">The game time.</param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(Texture, new Rectangle((int)Position.X, -(int)Position.Y, 60, 30), Color);
        }
    }
}

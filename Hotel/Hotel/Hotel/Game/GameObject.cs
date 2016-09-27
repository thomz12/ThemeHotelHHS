using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class HotelObject
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public Rectangle BoundingBox { get; set; }

        private ContentManager _content;

        public event EventHandler Click;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager</param>
        public HotelObject(ContentManager content)
        {
            _content = content;
            BoundingBox = new Rectangle();
            Sprite = new Sprite(content);
        }

        public void OnClick(EventArgs e)
        {
            if(Click != null)
                Click(this, e);
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">Time since last frame.</param>
        public virtual void Update(float deltaTime)
        {
            Sprite.SetPosition(new Point((int)Position.X, (int)Position.Y));
            Sprite.Update(deltaTime);
        }

        /// <summary>
        /// Called when drawing to the sprite batch.
        /// </summary>
        /// <param name="batch">The sprite batch to draw to.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);
        }
    }
}

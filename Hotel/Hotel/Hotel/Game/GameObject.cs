using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class GameObject
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public Rectangle BoundingBox { get; set; }

        private ContentManager _content;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager</param>
        public GameObject(ContentManager content)
        {
            _content = content;
            BoundingBox = new Rectangle();
            Sprite = new Sprite(content);
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">Time since last frame.</param>
        public virtual void Update(float deltaTime)
        {
            Sprite.Update(deltaTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="gameTime"></param>
        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);
        }
    }
}

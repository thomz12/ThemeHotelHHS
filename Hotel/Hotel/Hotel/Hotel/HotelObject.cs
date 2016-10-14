using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public abstract class HotelObject
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public Rectangle BoundingBox { get; set; }

        public event EventHandler RemoveObjectEvent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager</param>
        public HotelObject()
        {
            BoundingBox = new Rectangle();
            Sprite = new Sprite();

            Name = "HotelObject";
        }

        /// <summary>
        /// Call this to remove this object from the game.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        public virtual void Remove(EventArgs e)
        {
            // Call the event so the hotel can use it.
            if (RemoveObjectEvent != null)
                RemoveObjectEvent(this, e);
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

        public override string ToString()
        {
            return $"{Name}, {Position}";
        }
    }
}

﻿using Microsoft.Xna.Framework;
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
        public Rectangle DrawDestination { get; set; }
        public Color Color { get; set; }
        public float DrawOrder{ get; set; }

        private ContentManager _content;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">The path to the file to load.</param>
        /// <param name="content">The content manager used to load in images.</param>
        public Sprite()
        {
            _content = ServiceLocator.Get<ContentManager>();

            DrawOrder = 0;
            Color = Color.White;

            DrawDestination = new Rectangle(0, 0, 0, 0);
        }

        /// <summary>
        /// Loads the texture used for the sprite.
        /// </summary>
        /// <param name="path">Path to the image</param>
        public void LoadSprite(string path)
        {
            Texture = _content.Load<Texture2D>(path);
        }

        /// <summary>
        /// Sets the position where the object should be drawn at.
        /// </summary>
        /// <param name="position">The position of the object.</param>
        public void SetPosition(Point position)
        {
            DrawDestination = new Rectangle(position.X, -position.Y, DrawDestination.Width, DrawDestination.Height);
        }
        
        /// <summary>
        /// Sets the size of the object to draw.
        /// </summary>
        /// <param name="size">The size of the object.</param>
        public void SetSize(Point size)
        {
            DrawDestination = new Rectangle(DrawDestination.X, DrawDestination.Y, size.X, size.Y);
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="deltaTime">The game time.</param>
        public void Update(float deltaTime)
        {
            
        }

        /// <summary>
        /// Call this to draw the object on the screen.
        /// </summary>
        /// <param name="batch">The batch to draw to.</param>
        /// <param name="gameTime">The game time.</param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if(Texture != null)
                batch.Draw(Texture, DrawDestination, new Rectangle(0, 0, Texture.Width, Texture.Height), Color, 0, Vector2.Zero, SpriteEffects.None, DrawOrder);
        }
    }
}
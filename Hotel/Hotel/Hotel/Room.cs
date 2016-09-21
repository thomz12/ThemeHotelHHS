using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class Room
    {
        // SPRITE
        public int Width { get; set; }
        public Vector2 Position { get; set; }
        public int Weight { get; set; }

        Texture2D temp;

        public Room(ContentManager content)
        {
            temp = content.Load<Texture2D>("img");
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(temp, new Rectangle((int)Position.X, (int)Position.Y, 60, 30), Color.White);
        }
    }
}
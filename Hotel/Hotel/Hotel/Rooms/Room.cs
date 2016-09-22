using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public abstract class Room
    {
        public Sprite Sprite { get; private set; }

        public Vector2 Position { get; set; }
        public int Weight { get; set; }

        public Room(ContentManager content)
        {
            Sprite = new Sprite("img", content);
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(Position, gameTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);
        }
    }
}
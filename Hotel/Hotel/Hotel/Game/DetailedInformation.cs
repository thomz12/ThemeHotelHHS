using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Hotel
{
    public class DetailedInformation
    {
        private SpriteFont _spriteFont;

        public void Detailedinformation(ContentManager content)
        {
            _spriteFont = content.Load<SpriteFont>("ArcadeClassic");
        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {

        }
    }
}

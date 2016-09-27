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
        private HotelObject _object;

        public void Detailedinformation(ContentManager content, HotelObject DisplayThis)
        {
            _spriteFont = content.Load<SpriteFont>("ArcadeClassic");
            _object = DisplayThis;
        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {

        }
    }
}

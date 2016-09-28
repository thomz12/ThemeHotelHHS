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
        private Sprite _sprite;
        private bool _isShowingInfo;
        private string _displayThis;

        public DetailedInformation(ContentManager content)
        {
            _spriteFont = content.Load<SpriteFont>("Default");
            _sprite = new Sprite(content);
            _sprite.LoadSprite("GUIDetailWindowBackSprite");

            _displayThis = "<Null>";

            _sprite.SetSize(new Point(200, 100));
            _sprite.SetPosition(new Point(0, 0));
        }

        public void ShowInformation(HotelObject objectToDisplay)
        {
            string _displayThis = objectToDisplay.ToString();

            _isShowingInfo = true;
        }

        public void HideInformation()
        {
            _isShowingInfo = false;
        }

        public void Update(float deltaTime)
        {
            _sprite.Update(deltaTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (_isShowingInfo)
            {
                _sprite.Draw(batch, gameTime);
                
                Vector2 position = new Vector2(0, 0);
                batch.DrawString(_spriteFont, _displayThis, position, Color.Black, 0f, new Vector2(0,0), 0f, SpriteEffects.None, 1);
            }
        }
    }
}

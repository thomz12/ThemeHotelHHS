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
        public float Scale { get; set; }
        public bool IsShowingInfo{ get; private set;}

        private SpriteFont _spriteFont;
        private Sprite _sprite;
        private string _displayThis;

        public DetailedInformation(ContentManager content)
        {
            Scale = 0.5f;

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

            IsShowingInfo = true;
        }

        public void HideInformation()
        {
            IsShowingInfo = false;
        }

        public void Update(float deltaTime)
        {
            _sprite.Update(deltaTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (IsShowingInfo)
            {
                _sprite.Draw(batch, gameTime);
                
                Vector2 position = new Vector2(0, 0);
                batch.DrawString(_spriteFont, _displayThis, position, Color.Black, 0f, new Vector2(0,0), Scale, SpriteEffects.None, 1);
            }
        }
    }
}

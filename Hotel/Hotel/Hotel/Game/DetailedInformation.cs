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
        public Point WindowSize { get; set; }
        public Point WindowPosition { get; set; }
        public bool IsShowingInfo{ get; private set;}
        public float Intensity { get; set; }

        private SpriteFont _spriteFont;
        private Sprite _sprite;
        private string _objectName;
        private string _objectInformation;

        public DetailedInformation(ContentManager content)
        {
            Scale = 0.5f;
            WindowSize = new Point(300, 400);
            WindowPosition = new Point(0, 0);
            Intensity = 0.8f;

            _spriteFont = content.Load<SpriteFont>("Default");
            _sprite = new Sprite(content);
            _sprite.LoadSprite("GUIDetailWindowBackSprite");
            _sprite.Color *= Intensity;

            _objectInformation = "<Null>";

            _sprite.SetSize(WindowSize);
            _sprite.SetPosition(WindowPosition);
        }

        public void ShowInformation(HotelObject objectToDisplay)
        {
            _objectName = objectToDisplay.ToString().Split(';')[0];
            _objectInformation = objectToDisplay.ToString().Split(';')[1];

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
                batch.DrawString(_spriteFont, _objectInformation, position, Color.Black, 0f, new Vector2(0,0), Scale, SpriteEffects.None, 1);
            }
        }
    }
}

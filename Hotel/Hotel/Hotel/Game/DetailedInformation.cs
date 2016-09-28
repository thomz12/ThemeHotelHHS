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
        private Sprite _windowSprite;
        private Sprite _headerSprite;
        private string _objectName;
        private string _objectInformation;

        public DetailedInformation(ContentManager content)
        {
            // Set some default values for properties
            Scale = 0.5f;
            WindowSize = new Point(300, 400);
            WindowPosition = new Point(10, 10);
            Intensity = 0.8f;

            // Load the font
            _spriteFont = content.Load<SpriteFont>("Default");

            // Load the asset for the window's background
            _windowSprite = new Sprite(content);
            _windowSprite.LoadSprite("GUIDetailWindowBackSprite");
            _windowSprite.Color *= 1.0f;

            // Load another asset for the window's name bar
            _headerSprite = new Sprite(content);
            _headerSprite.LoadSprite("GUIDetailWindowBackSprite");
            _headerSprite.Color *= Intensity;

            // Set some default values for privates
            _objectInformation = "<Null>";

            // Set the window Size and Position
            _windowSprite.SetSize(WindowSize);
            _windowSprite.SetPosition(new Point(WindowPosition.X, WindowPosition.Y * -1));

            // Set the header Size and Position relative to window size.
            _headerSprite.SetSize(new Point(WindowSize.X - 10, 40));
            _headerSprite.SetPosition(new Point(WindowPosition.X + 5, WindowPosition.Y - 20));
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
            _windowSprite.Update(deltaTime);
            _headerSprite.Update(deltaTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (IsShowingInfo)
            { 
                _windowSprite.Draw(batch, gameTime);
                _headerSprite.Draw(batch, gameTime);

                Vector2 namePos = new Vector2(WindowPosition.X + WindowSize.X / 2, WindowPosition.Y + 20);
                Vector2 nameOrigin = _spriteFont.MeasureString(_objectName) / 2;
                batch.DrawString(_spriteFont, _objectName, namePos, Color.Black, 0f, nameOrigin, Scale, SpriteEffects.None, 1);
                
                Vector2 infoPos = new Vector2(WindowPosition.X + 20, WindowPosition.Y + 250);
                batch.DrawString(_spriteFont, _objectInformation, infoPos, Color.Black, 0f, new Vector2(0,0), Scale, SpriteEffects.None, 1);
            }
        }
    }
}

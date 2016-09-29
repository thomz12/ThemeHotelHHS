using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Hotel.Persons;

namespace Hotel
{
    public class DetailedInformation
    {
        public bool IsShowingInfo{ get; private set;}
        public float TitleTextSize { get; set; }
        public float InfoTextSize { get; set; }
        public Texture2D texture { get; set; }
        public Point WindowSize { get; set; }
        public Point WindowPosition { get; set; }
        public Color TextColor { get; set; }

        private HotelObject _objectToDisplay;
        private SpriteFont _spriteFont;
        private Sprite _windowSprite;
        private string _objectName;
        private string _objectInformation;
        private Point _titleOffset;
        private Point _infoOffset;

        public DetailedInformation(ContentManager content)
        {
            // Set some default values for properties
            TitleTextSize = 0.7f;
            InfoTextSize = 0.5f;
            WindowSize = new Point(300, 500);
            WindowPosition = new Point(0, 0);

            // Set the offset for the text
            _titleOffset = new Point(0, 50);
            _infoOffset = new Point(45, 310);

            // Load the font
            _spriteFont = content.Load<SpriteFont>("InformationWindowFont");

            // Set the text color
            TextColor = Color.DarkSlateBlue;

            // Load the asset for the window's background
            _windowSprite = new Sprite(content);
            _windowSprite.LoadSprite("PaperClean");
            _windowSprite.DrawOrder = 0.0f;

            // Set some default values for privates
            _objectName = "<Null>";
            _objectInformation = "Something went wrong!";

            // Set the window Size and Position
            _windowSprite.SetSize(WindowSize);
            _windowSprite.SetPosition(new Point(WindowPosition.X, WindowPosition.Y * -1));
        }

        /// <summary>
        /// Show the information window with text from a specific object.
        /// </summary>
        /// <param name="objectToDisplay">The object to display information about.</param>
        public void ShowInformation(HotelObject objectToDisplay)
        {
            _objectToDisplay = objectToDisplay;

            IsShowingInfo = true;
        }

        /// <summary>
        /// Hide the information window.
        /// </summary>
        public void HideInformation()
        {
            // Unred the target room
            if(_objectToDisplay is Person)
            {
                Person person = (Person)_objectToDisplay;
                foreach (Room room in person.Path)
                {
                    room.Sprite.Color = Color.White;
                }

                person.TargetRoom.Sprite.Color = Color.White;
            }

            IsShowingInfo = false;
        }

        public void Update(float deltaTime)
        {
            _windowSprite.Update(deltaTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            // Check if the window may actually be drawn.
            if (IsShowingInfo)
            {
                // Make the target room red and the path room blue
                if (_objectToDisplay is Person)
                {
                    Person person = (Person)_objectToDisplay;
                    foreach (Room room in person.Path)
                    {
                        room.Sprite.Color = Color.DarkGray;
                    }

                    person.CurrentRoom.Sprite.Color = Color.White;
                    person.TargetRoom.Sprite.Color = Color.Red;
                }

                // Split the spring into a Title and Information part, which are drawn seperately.
                string[] appropriateName = _objectToDisplay.ToString().Split(';');
                _objectName = appropriateName[0];
                _objectInformation = appropriateName[1];
                
                // Draw the background for the window
                _windowSprite.Draw(batch, gameTime);

                // Unleash some calculations to determine the center of the window and string, to center the title text.
                Vector2 namePos = new Vector2(WindowPosition.X + WindowSize.X / 2 + _titleOffset.X, WindowPosition.Y + _titleOffset.Y);
                Vector2 nameOrigin = _spriteFont.MeasureString(_objectName) / 2;
                // Use the above vectors to change the position of the object.
                batch.DrawString(_spriteFont, _objectName, namePos, TextColor, 0f, nameOrigin, TitleTextSize, SpriteEffects.None, 1);

                batch.Draw(texture, new Rectangle(WindowPosition.X + 60, WindowPosition.Y + 80, texture.Width, texture.Height), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);

                // Do the same as the above for the information, but then without centering the text.
                Vector2 infoPos = new Vector2(WindowPosition.X + _infoOffset.X, WindowPosition.Y + _infoOffset.Y);
                batch.DrawString(_spriteFont, _objectInformation, infoPos, TextColor, 0f, new Vector2(0,0), InfoTextSize, SpriteEffects.None, 1);
            }
        }
    }
}

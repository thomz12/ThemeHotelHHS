using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hotel
{
    public class ElevatorShaft : Room
    {
        public int Floor { get; private set; }

        private Elevator _elevator;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in images</param>
        public ElevatorShaft(ContentManager content, int floor) : base(content)
        {
            Sprite.LoadSprite("ElevatorRoom");
            Floor = floor;

            if (Floor == 0)
            {
                _elevator = new Elevator(content);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(Position, gameTime);

            if (Floor == 0)
            {
                _elevator.Position = new Vector2(Position.X + Sprite.Texture.Width - _elevator.Sprite.Texture.Width, _elevator.Position.Y);
                _elevator.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);

            if (Floor == 0)
            {
                _elevator.Draw(batch, gameTime);
            }
        }
    }
}

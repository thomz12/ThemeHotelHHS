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
        private Elevator _elevator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in images</param>
        public ElevatorShaft(ContentManager content, Point position) : base(content, position, new Point(1, 1))
        {
            Sprite.LoadSprite("ElevatorRoom");

            if (RoomPosition.Y == 0)
            {
                _elevator = new Elevator(content);
            }
        }

        public override void Update(float deltaTime)
        {
            Sprite.Update(deltaTime);

            if (RoomPosition.Y == 0)
            {
                _elevator.Position = new Vector2(((RoomPosition.X + RoomSize.X) * ROOMWIDTH)- _elevator.Sprite.Texture.Width, _elevator.Position.Y);
                _elevator.Update(deltaTime);
            }
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);

            if (RoomPosition.Y == 0)
            {
                _elevator.Draw(batch, gameTime);
            }
        }
    }
}

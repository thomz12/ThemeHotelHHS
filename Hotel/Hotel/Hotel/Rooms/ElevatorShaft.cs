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
        public Elevator Elevator { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in images</param>
        public ElevatorShaft(ContentManager content, Point position) : base(content, position, new Point(1, 1))
        {
            Sprite.LoadSprite("ElevatorRoom");

            Vertical = true;

            if (RoomPosition.Y == 0)
            {
                Elevator = new Elevator(content);
            }
        }

        public void CallElevator(int targetFloor)
        {
            Elevator.CallElevator(RoomPosition.Y, targetFloor);
        }

        public override void Update(float deltaTime)
        {
            Sprite.Update(deltaTime);

            // Update elevator positions
            if (RoomPosition.Y == 0)
            {
                Elevator.Position = new Vector2(((RoomPosition.X + RoomSize.X) * ROOMWIDTH)- Elevator.Sprite.Texture.Width, Elevator.Position.Y);
                Elevator.Update(deltaTime);
            }

            if(Elevator == null)
            {
                ElevatorShaft es = (ElevatorShaft)Neighbors[Direction.South];
                Elevator = es.Elevator;
            }
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);

            if (RoomPosition.Y == 0)
            {
                Elevator.Draw(batch, gameTime);
            }
        }
    }
}

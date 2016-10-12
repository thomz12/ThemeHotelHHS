using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hotel
{
    public class ElevatorShaft : Room
    {
        public Elevator Elevator { get; private set; }

        public event EventHandler ElevatorArrival;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        /// <param name="position">The position of the room.</param>
        public ElevatorShaft(int id, Point position, float elevatorSpeed) : base(id, position, new Point(1, 1))
        {
            Sprite.LoadSprite("1x1ElevatorShaft");
            Name = "Elevator Shaft";
            Weight = 1;
            Vertical = true;

            if (RoomPosition.Y == 0)
            {
                Elevator = new Elevator(elevatorSpeed);
                Elevator.Arrival += Elevator_Arrival;
            }
        }

        private void OnElevatorArrival(EventArgs args)
        {
            if (ElevatorArrival != null)
                ElevatorArrival(Elevator, args);
        }

        private void Elevator_Arrival(object sender, EventArgs e)
        {
            if((sender as Elevator).CurrentFloor == RoomPosition.Y)
            {
                OnElevatorArrival(e);
            }
        }

        /// <summary>
        /// Calls the elevator on this floor
        /// </summary>
        /// <param name="targetFloor">The floor to send the elevator to after it arrived on this floor (the destination, or target)</param>
        public void CallElevator(int targetFloor)
        {
            Elevator.CallElevator(RoomPosition.Y, targetFloor);
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {
            Sprite.Update(deltaTime);

            // Update elevator positions
            if (RoomPosition.Y == 0)
            {
                Elevator.Position = new Vector2(((RoomPosition.X + RoomSize.X) * ROOMWIDTH)- Elevator.Sprite.Texture.Width, Elevator.Position.Y);
                Elevator.Update(deltaTime);
            }
            
            if(Elevator == null && RoomPosition.Y != 0)
            {
                ElevatorShaft es = (ElevatorShaft)Neighbors[Direction.South];
                Elevator = es.Elevator;
                Elevator.Arrival += Elevator_Arrival;
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

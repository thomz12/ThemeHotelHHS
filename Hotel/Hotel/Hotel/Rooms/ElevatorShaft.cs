﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hotel.RoomBehaviours;

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
        public ElevatorShaft(int id, Point position) : base(id, position, new Point(1, 1))
        {
            Sprite.LoadSprite("1x1ElevatorShaft");
            Name = "Elevator Shaft";
            Weight = 1;
            Vertical = true;

            // Create a roombehaviour.
            RoomBehaviour = new ElevatorShaftBehaviour();

            // If this is the bottom floor, create an elevator.
            if (RoomPosition.Y == 0)
            {
                Elevator = new Elevator();
                Elevator.Arrival += Elevator_Arrival;
            }
        }

        private void OnElevatorArrival(EventArgs args)
        {
            if (ElevatorArrival != null)
                ElevatorArrival(Elevator, args);
        }

        /// <summary>
        /// Called when the elevator arrives on a floor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elevator_Arrival(object sender, EventArgs e)
        {
            // Check if the elevator arrived on THIS floor.
            if((sender as Elevator).CurrentFloor == RoomPosition.Y)
            {
                // if it did, call the on elevator arrive event.
                OnElevatorArrival(e);
            }
        }

        /// <summary>
        /// Calls the elevator on this floor
        /// </summary>
        /// <param name="dir">the direction to go up = going up, down = going down and both = most likely target floor.</param>
        public void CallElevator(ElevatorDirection dir)
        {
            Elevator.CallElevator(RoomPosition.Y, dir);
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">The time passed last frame.</param>
        public override void Update(float deltaTime)
        {
            Sprite.Update(deltaTime);

            // Update elevator positions
            if (RoomPosition.Y == 0)
            {
                int width = 0;
                if (Elevator.Sprite.Texture != null)
                    width = Elevator.Sprite.Texture.Width;

                Elevator.Position = new Vector2(((RoomPosition.X + RoomSize.X) * ROOMWIDTH) - width, Elevator.Position.Y);
                Elevator.Update(deltaTime);
            }
            
            if(Elevator == null && RoomPosition.Y != 0)
            {
                ElevatorShaft es = (ElevatorShaft)Neighbors[Direction.South];
                Elevator = es.Elevator;
                Elevator.Arrival += Elevator_Arrival;
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (RoomPosition.Y == 0)
            {
                Elevator.Draw(batch, gameTime);
            }

            base.Draw(batch, gameTime);
        }
    }
}

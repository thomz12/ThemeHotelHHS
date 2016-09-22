using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public enum ElevatorState
    {
        Idle,
        GoingUp,
        GoingDown,
        Stopped
    }

    public enum ElevatorDirection
    {
        Up,
        Down
    }

    public class Elevator
    {
        public Sprite Sprite { get; private set; }
        public Vector2 Position { get; set; }

        public ElevatorState State { get; private set; }

        private int _currentFloor;


        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Elevator(ContentManager content)
        {
            Sprite = new Sprite(content);
            Sprite.LoadSprite("Elevator");
            _currentFloor = 0;
            State = ElevatorState.Idle;
        }

        /// <summary>
        /// Called by the elevator shaft
        /// </summary>
        /// <param name="direction">The direction the elevator needs to go</param>
        public void CallElevator(ElevatorDirection direction)
        {

        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            Sprite.Update(Position, gameTime);
            Position = new Vector2(Position.X, Position.Y + 1);
        }

        /// <summary>
        /// Called when drawing to the screen.
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);
        }
    }
}

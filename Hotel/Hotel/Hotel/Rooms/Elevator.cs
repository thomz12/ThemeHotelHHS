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
        public float Speed { get; set; }

        public ElevatorState State { get; private set; }

        public float WaitTime { get; set; }

        private int _currentFloor;
        private float _waitTime;
        private int _targetFloor;
        private Dictionary<int, ElevatorDirection> _queue;

        private static Random r = new Random();

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Elevator(ContentManager content)
        {
            Sprite = new Sprite(content);
            Sprite.LoadSprite("Elevator");
            Sprite.DrawOrder = 1;
            WaitTime = 2.0f;
            Speed = 8.0f;
            State = ElevatorState.Idle;

            _currentFloor = 0;
            _queue = new Dictionary<int, ElevatorDirection>();

            // Create a random queue
            for (int i = 0; i < 100; i++)
            {
                int floor = r.Next(0, 100);

                if(_queue.ContainsKey(floor))
                {
                    i--;
                    continue;
                }
                _queue.Add(floor, ElevatorDirection.Up);
            }
        }

        /// <summary>
        /// Called by the elevator shaft
        /// </summary>
        /// <param name="direction">The direction the elevator needs to go</param>
        public void CallElevator(int floor, ElevatorDirection direction)
        {
            _queue.Add(floor, direction);
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {

            Sprite.Update(Position, gameTime);

            if (_waitTime <= 0)
            {
                if (_queue.Keys.Count > 0)
                    MoveToFloor(90 * _queue.Keys.ElementAt(0));
            }
            else
            {
                _waitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void MoveToFloor(int height)
        {
            if (Position.Y > height)
                Position = new Vector2(Position.X, Position.Y - Speed);
            else
                Position = new Vector2(Position.X, Position.Y + Speed);

            if (Math.Abs(Position.Y - height) < Speed)
            {
                Position = new Vector2(Position.X, height);
                _queue.Remove(height / 90);

                _waitTime = WaitTime;
            }
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

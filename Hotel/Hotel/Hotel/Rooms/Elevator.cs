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
            State = ElevatorState.GoingUp;

            _currentFloor = 0;
            _queue = new Dictionary<int, ElevatorDirection>();

            // Create a random queue
            for (int i = 0; i < 10; i++)
            {
                int floor = r.Next(0, 10);

                if(_queue.ContainsKey(floor))
                {
                    i--;
                    continue;
                }
                _queue.Add(floor, (ElevatorDirection)r.Next(0, 2));
            }
        }

        public int GetTargetFloor()
        {
            int floor = 0;

            if (State == ElevatorState.GoingUp)
            {
                try
                {
                    floor = _queue.Keys.Where(x => x > _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Up)).OrderBy(x => x).First();
                }
                catch
                {
                    State = ElevatorState.GoingDown;
                    floor = GetTargetFloor();
                }
            }
            else if(State == ElevatorState.GoingDown)
            {
                try
                {
                    floor = _queue.Keys.Where(x => x < _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).FirstOrDefault();
                }
                catch
                {
                    State = ElevatorState.GoingUp;
                    floor = GetTargetFloor();
                }
            }

            return floor;
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
                    MoveToFloor(90 * _targetFloor);
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
                _queue.Remove(_targetFloor);

                _currentFloor = _targetFloor;
                _targetFloor = GetTargetFloor();

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

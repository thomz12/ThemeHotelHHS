﻿using Microsoft.Xna.Framework;
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

    [Flags]
    public enum ElevatorDirection
    {
        Up = 1,
        Down = 2,
        Both = 3
    }

    /// <summary>
    /// An elevator that can go up or down and can get called by an elevator shaft to come pick up people and drop them off at their destination.
    /// </summary>
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
            WaitTime = 1.0f;
            Speed = 32.0f;
            State = ElevatorState.GoingUp;

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
                _queue.Add(floor, (ElevatorDirection)r.Next(0, 2));
            }
        }

        /// <summary>
        /// The algorithm to check which floor to go to next is in this function.
        /// </summary>
        /// <returns>The floor that the elevator needs to travel to according to the algorithm.</returns>
        private int GetTargetFloor()
        {
            int floor = 0;

            // If the queue is empty
            if(_queue.Count == 0)
            {
                // Set the elevator idle.
                State = ElevatorState.Idle;
                // Our target floor is the current floor.
                return _currentFloor;
            }

            // If the elevator is going up
            if (State == ElevatorState.GoingUp)
            {
                try
                {
                    // Try to find a floor above the elevator, that wants to go up.
                    floor = _queue.Keys.Where(x => x > _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Up)).OrderBy(x => x).First();
                }
                catch
                {
                    // Nobody needs to go up anymore, but we still need to pick up the people who are some stories higher and want to go down.
                    try
                    {
                        // We pick the request on the highest floor for going down.
                        floor = _queue.Keys.Where(x => x > _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).First();
                    }
                    catch
                    {
                        // There is no request available so the elevator's direction must turn around.
                        State = ElevatorState.GoingDown;
                        floor = GetTargetFloor();
                    }
                }
            }
            // If the elevator is going down
            else if(State == ElevatorState.GoingDown)
            {
                try
                {
                    // Try to find a floor below the elevator, that wants to go down.
                    floor = _queue.Keys.Where(x => x < _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).First();
                }
                catch
                {
                    // Nobody needs to go down anymore, but we sitll need to pick up people who are lower in the building and want to go up.
                    try
                    {
                        // We go to the person who is on the lowest floor and wants to go up.
                        floor = _queue.Keys.Where(x => x < _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Up)).OrderBy(x => x).First();
                    }
                    catch
                    {
                        // There is no request available anymore so the elevator's direction must turn around.
                        State = ElevatorState.GoingUp;
                        floor = GetTargetFloor();
                    }
                }
            }
            else if(State == ElevatorState.Idle)
            {
                // When the elevator is idle, go to the request that comes from the nearest floor (in case two requests are ordered at once)
                floor = _queue.Keys.Where(x => x <= _currentFloor).Min(x => _currentFloor - x);

                // Change the state of the elevator according to which way it is going.
                if (floor > _currentFloor)
                    State = ElevatorState.GoingUp;
                else if(floor < _currentFloor)
                    State = ElevatorState.GoingDown;
            }

            return floor;
        }

        /// <summary>
        /// Call the elevator to do something.
        /// </summary>
        /// <param name="floor">The floor from which the call comes.</param>
        /// <param name="direction">The direction the elevator needs to travel.</param>
        public void CallElevator(int floor, ElevatorDirection direction)
        {
            // TODO: USE DESTINATIONS

            if (_queue.ContainsKey(floor))
                _queue[floor] |= direction;
            else
                _queue.Add(floor, direction);

            _targetFloor = GetTargetFloor();
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
                MoveToFloor(90 * _targetFloor);
            }
            else
            {
                _waitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        /// <summary>
        /// Moves the elevator to the given floor.
        /// </summary>
        /// <param name="height">The floor to move the elevator to.</param>
        public void MoveToFloor(int height)
        {
            // When the elevator reached its destination
            if (Math.Abs(Position.Y - height) < Speed)
            {
                Position = new Vector2(Position.X, height);

                ElevatorDirection direction = _queue[_targetFloor];
                // ?
                if(direction == ElevatorDirection.Up)
                {
                    CallElevator(r.Next(_targetFloor, 100), ElevatorDirection.Both);
                }
                else if(direction == ElevatorDirection.Down)
                {
                    CallElevator(r.Next(0, _targetFloor), ElevatorDirection.Both);
                }
                else
                {
                    ;
                }

                _queue.Remove(_targetFloor);

                _currentFloor = _targetFloor;
                _targetFloor = GetTargetFloor();

                _waitTime = WaitTime;

                return;
            }

            // Going up
            if (Position.Y > height)
                Position = new Vector2(Position.X, Position.Y - Speed);
            // Going down
            else
                Position = new Vector2(Position.X, Position.Y + Speed);
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

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
        private Dictionary<int?, ElevatorDirection> _queue;

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
            Speed = 90.0f;
            State = ElevatorState.Idle;

            _currentFloor = 0;
            _queue = new Dictionary<int?, ElevatorDirection>();

            // Create a random queue
            /*
            for (int i = 0; i < 1; i++)
            {
                int floor = r.Next(0, 10);

                if(_queue.ContainsKey(floor))
                {
                    i--;
                    continue;
                }
                CallElevator(floor, (ElevatorDirection)r.Next(0, 2));
            }*/
            CallElevator(4, 3);
            CallElevator(7, 0);
            CallElevator(6, 9);
        }

        /// <summary>
        /// The algorithm to check which floor to go to next is in this function.
        /// </summary>
        /// <returns>The floor that the elevator needs to travel to according to the algorithm.</returns>
        private int GetTargetFloor()
        {
            int? floor = 0;

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
                // Try to find a floor above the elevator, that wants to go up.
                floor = _queue.Keys.Where(x => x.Value > _currentFloor).Where(x => _queue[x.Value].HasFlag(ElevatorDirection.Up)).OrderBy(x => x.Value).FirstOrDefault();
                if(floor == null)
                { 
                    // We pick the request on the highest floor for going down.
                    floor = _queue.Keys.Where(x => x > _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).FirstOrDefault();

                    if (floor == null)
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
                // Try to find a floor below the elevator, that wants to go down.
                floor = _queue.Keys.Where(x => x < _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).FirstOrDefault();

                if(floor == null)
                {
                    // Nobody needs to go down anymore, but we sitll need to pick up people who are lower in the building and want to go up.
                    // We go to the person who is on the lowest floor and wants to go up.
                    floor = _queue.Keys.Where(x => x < _currentFloor).Where(x => _queue[x].HasFlag(ElevatorDirection.Up)).OrderBy(x => x).FirstOrDefault();

                    if(floor == null)
                    {
                        // There is no request available anymore so the elevator's direction must turn around.
                        State = ElevatorState.GoingUp;
                        floor = GetTargetFloor();
                    }
                }
            }
            else if(State == ElevatorState.Idle)
            {
                floor = _queue.Keys.Aggregate((x,y) => Math.Abs(x.Value -_currentFloor) < Math.Abs(y.Value - _currentFloor) ? x : y);

                // Change the state of the elevator according to which way it is going.
                if (floor > _currentFloor)
                    State = ElevatorState.GoingUp;
                else if(floor < _currentFloor)
                    State = ElevatorState.GoingDown;
            }

            return floor.HasValue ? floor.Value : _currentFloor;
        }

        /// <summary>
        /// Call the elevator to do something.
        /// </summary>
        /// <param name="floor">The floor from which the call comes.</param>
        /// <param name="direction">The direction the elevator needs to travel.</param>
        public void CallElevator(int floor, int targetFloor)
        {
            ElevatorDirection dir;

            if (floor > targetFloor)
                dir = ElevatorDirection.Down;
            else if (floor < targetFloor)
                dir = ElevatorDirection.Up;
            else
                dir = ElevatorDirection.Both;

            if (_queue.ContainsKey(floor))
                _queue[floor] |= dir;
            else
                _queue.Add(floor, dir);

            if (_queue.ContainsKey(targetFloor))
                _queue[targetFloor] = ElevatorDirection.Both;
            else
                _queue.Add(targetFloor, ElevatorDirection.Both);

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
                if(_queue.Count > 0)
                MoveToFloor(90 * _targetFloor, gameTime);
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
        public void MoveToFloor(int height, GameTime gameTime)
        {
            // When the elevator reached its destination
            if (Math.Abs(Position.Y - height) < Speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                Position = new Vector2(Position.X, height);
                _queue.Remove(_targetFloor);

                _currentFloor = _targetFloor;
                _targetFloor = GetTargetFloor();

                _waitTime = WaitTime;

                return;
            }

            // Going up
            if (Position.Y > height)
                Position = new Vector2(Position.X, Position.Y - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            // Going down
            else
                Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
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

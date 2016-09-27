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
    public class Elevator : HotelObject
    {
        public float Speed { get; set; }
        public ElevatorState State { get; private set; }
        public float WaitTime { get; set; }

        private int _currentFloor;
        private float _waitTime;
        private int _targetFloor;
        private Dictionary<int?, ElevatorDirection> _queue;
        private Dictionary<int, int> _queueTarget;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Elevator(ContentManager content) : base(content)
        {
            Sprite.LoadSprite("Elevator");
            Sprite.DrawOrder = 1;
            WaitTime = 1.0f;
            Speed = 90.0f;
            State = ElevatorState.Idle;

            Sprite.SetSize(new Point(Sprite.Texture.Width, Sprite.Texture.Height));

            _currentFloor = 0;
            _queue = new Dictionary<int?, ElevatorDirection>();
            _queueTarget = new Dictionary<int, int>();
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
                floor = _queue.Keys.Where(x => x.Value > _currentFloor && _queue[x.Value].HasFlag(ElevatorDirection.Up)).OrderBy(x => x.Value).FirstOrDefault();
                if(floor == null)
                { 
                    // We pick the request on the highest floor for going down.
                    floor = _queue.Keys.Where(x => x > _currentFloor && _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).FirstOrDefault();

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
                floor = _queue.Keys.Where(x => x < _currentFloor && _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).FirstOrDefault();

                if(floor == null)
                {
                    // Nobody needs to go down anymore, but we sitll need to pick up people who are lower in the building and want to go up.
                    // We go to the person who is on the lowest floor and wants to go up.
                    floor = _queue.Keys.Where(x => x < _currentFloor && _queue[x].HasFlag(ElevatorDirection.Up)).OrderBy(x => x).FirstOrDefault();

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
                // If the elevator is idle, find the closest target.
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
            // Calculate if the up or down button was pressed.
            ElevatorDirection dir;

            if (floor > targetFloor)
                dir = ElevatorDirection.Down;
            else if (floor < targetFloor)
                dir = ElevatorDirection.Up;
            else
                dir = ElevatorDirection.Both;

            // if the floor is already added to the queue, add the new direction to it.
            if (_queue.ContainsKey(floor))
                _queue[floor] |= dir;
            // else add the floor.
            else
                _queue.Add(floor, dir);

            // Save the destination for adding later to the queue (when guest enters the room).
            if(!_queueTarget.ContainsKey(floor))
                _queueTarget.Add(floor, targetFloor);

            _targetFloor = GetTargetFloor();

            // GLORIUS CONSOLE PRINT MASTERRACE.
            Console.WriteLine($"Call {floor} -> Target {targetFloor}");
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">The game time.</param>
        public override void Update(float deltaTime)
        {
            Sprite.Update(deltaTime);

            Sprite.SetPosition(new Point((int)Position.X, (int)Position.Y));

            if (_waitTime <= 0)
            {
                if(_queue.Count > 0)
                    MoveToFloor(_targetFloor, deltaTime);
            }
            else
            {
                _waitTime -= deltaTime;
            }
        }

        /// <summary>
        /// Moves the elevator to the given floor.
        /// </summary>
        /// <param name="floor">The floor to move the elevator to.</param>
        private void MoveToFloor(int floor, float deltaTime)
        {
            // When the elevator has yet to reach its destination
            if (Math.Abs(Position.Y - floor * Sprite.Texture.Height) < Speed * deltaTime)
            {
                // Set the position on the exact floor position.
                Position = new Vector2(Position.X, floor * Sprite.Texture.Height);
                _queue.Remove(_targetFloor);

                // The current floor is now the target.
                _currentFloor = _targetFloor;

                if(_queueTarget.ContainsKey(_currentFloor))
                {
                    if (_queue.ContainsKey(_queueTarget[_currentFloor]))
                        _queue[_queueTarget[_currentFloor]] = ElevatorDirection.Both;
                    else
                        _queue.Add(_queueTarget[_currentFloor], ElevatorDirection.Both);

                    _queueTarget.Remove(_currentFloor);
                }

                _targetFloor = GetTargetFloor();

                _waitTime = WaitTime;

                return;
            }

            // Going up
            if (Position.Y > floor * Sprite.Texture.Height)
                Position = new Vector2(Position.X, Position.Y - Speed * deltaTime);
            // Going down
            else
                Position = new Vector2(Position.X, Position.Y + Speed * deltaTime);
        }
    }
}

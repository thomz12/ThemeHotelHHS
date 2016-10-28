using Microsoft.Xna.Framework;
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
        public int CurrentFloor;

        private float _waitTime;
        private int _targetFloor;
        private Dictionary<int?, ElevatorDirection> _queue;

        // Arrival event gets called when the elevator arrives on a floor.
        public event EventHandler Arrival;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Elevator() : base()
        {
            Sprite.LoadSprite("Elevator");
            Name = "Elevator";
            Sprite.DrawOrder = 0.5f;
            WaitTime = 1.0f;
            State = ElevatorState.Idle;

            // Load settings from the config.
            Speed = Room.ROOMHEIGHT * ServiceLocator.Get<ConfigLoader>().GetConfig().ElevatorSpeed;

            if(Sprite.Texture != null)
                Sprite.SetSize(new Point(Sprite.Texture.Width, Room.ROOMHEIGHT));

            CurrentFloor = 0;
            _queue = new Dictionary<int?, ElevatorDirection>();
        }

        /// <summary>
        /// The algorithm to check which floor to go to next is in this function.
        /// </summary>
        /// <returns>The floor that the elevator needs to travel to according to the algorithm.</returns>
        private int GetTargetFloor()
        {
            int? floor = 0;

            CurrentFloor = (int)Math.Round((Position.Y / (float)Room.ROOMHEIGHT));

            // If the queue is empty
            if(_queue.Count == 0)
            {
                // Set the elevator idle.
                State = ElevatorState.Idle;
                // Our target floor is the current floor.
                return CurrentFloor;
            }

            // If the elevator is going up
            if (State == ElevatorState.GoingUp)
            {
                // Try to find a floor above the elevator, that wants to go up.
                floor = _queue.Keys.Where(x => x.Value > CurrentFloor && _queue[x.Value].HasFlag(ElevatorDirection.Up)).OrderBy(x => x.Value).FirstOrDefault();

                // if no more floor was found that wants to go up, 
                if(floor == null)
                { 
                    // We pick the request on the highest floor for going down.
                    floor = _queue.Keys.Where(x => x >= CurrentFloor && _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).FirstOrDefault();

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
                floor = _queue.Keys.Where(x => x < CurrentFloor && _queue[x].HasFlag(ElevatorDirection.Down)).OrderByDescending(x => x).FirstOrDefault();

                if(floor == null)
                {
                    // Nobody needs to go down anymore, but we sitll need to pick up people who are lower in the building and want to go up.
                    // We go to the person who is on the lowest floor and wants to go up.
                    floor = _queue.Keys.Where(x => x <= CurrentFloor && _queue[x].HasFlag(ElevatorDirection.Up)).OrderBy(x => x).FirstOrDefault();

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
                floor = _queue.Keys.Aggregate((x,y) => Math.Abs(x.Value -CurrentFloor) < Math.Abs(y.Value - CurrentFloor) ? x : y);

                // Change the state of the elevator according to which way it is going.
                if (floor > CurrentFloor)
                    State = ElevatorState.GoingUp;
                else if(floor < CurrentFloor)
                    State = ElevatorState.GoingDown;
            }

            return floor.HasValue ? floor.Value : CurrentFloor;
        }

        /// <summary>
        /// Call the arrival event.
        /// </summary>
        /// <param name="e"></param>
        private void OnArrival(EventArgs e)
        {
            if (Arrival != null)
                Arrival(this, e);
        }

        /// <summary>
        /// Call the elevator to do something.
        /// </summary>
        /// <param name="floor">The floor from which the call comes.</param>
        /// <param name="direction">The direction the elevator needs to travel.</param>
        public void CallElevator(int floor, ElevatorDirection direction)
        {
            // if the floor is already added to the queue, add the new direction to it.
            if (_queue.ContainsKey(floor))
                _queue[floor] |= direction;
            // else add the floor.
            else
                _queue.Add(floor, direction);

            _targetFloor = GetTargetFloor();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">The game time.</param>
        public override void Update(float deltaTime)
        {
            // Update the sprite.
            Sprite.Update(deltaTime);

            // When done waiting.
            if (_waitTime <= 0)
            {
                if(_queue.Count > 0)
                    MoveToFloor(_targetFloor, deltaTime);
            }
            else
            {
                _waitTime -= deltaTime;
            }

            // Set the Sprite position.
            Sprite.SetPosition(new Point((int)Position.X, (int)Position.Y));
        }

        /// <summary>
        /// Moves the elevator to the given floor.
        /// </summary>
        /// <param name="floor">The floor to move the elevator to.</param>
        private void MoveToFloor(int floor, float deltaTime)
        {
            // When the elevator has reached its destination
            if (Math.Abs(Position.Y - floor * Room.ROOMHEIGHT) < Speed * deltaTime)
            {
                // Set the position on the exact floor position.
                Position = new Vector2(Position.X, floor * Room.ROOMHEIGHT);
                _queue.Remove(_targetFloor);

                // The current floor is now the target.
                CurrentFloor = _targetFloor;

                // Call the OnArrival Event.
                OnArrival(new EventArgs());

                // Get the next floor.
                _targetFloor = GetTargetFloor();

                // Reset the waiting time.
                _waitTime = WaitTime;

                return;
            }

            // Going up
            if (Position.Y > floor * Room.ROOMHEIGHT)
                Position = new Vector2(Position.X, Position.Y - Speed * deltaTime);
            // Going down
            else
                Position = new Vector2(Position.X, Position.Y + Speed * deltaTime);
        }
    }
}

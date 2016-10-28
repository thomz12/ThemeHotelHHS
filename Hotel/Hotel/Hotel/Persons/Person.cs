using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;

namespace Hotel.Persons
{
    public enum PersonTask
    {
        Waiting,
        InQueue,
        MovingLeft,
        MovingRight,
        MovingUp,
        MovingDown,
        MovingCenter
    }

    public abstract class Person : HotelObject
    {
        public float WalkingSpeed { get; set; }

        private PersonTask _currentTask;
        public PersonTask CurrentTask
        {
            get
            {
                return _currentTask;
            }
            set
            {
                _currentTask = value;
                _deathTimer = 0;
            }
        }

        public Room CurrentRoom { get; protected set; }
        protected IRoomBehaviour _roomBehaviour;

        public bool Evacuating { get; set; }
        public bool Inside { get; set; }
        public Room TargetRoom { get; set; }
        public List<Room> Path { get; protected set; }
        public float JumpHeight { get; set; }
        // If this person already called the elevator on this floor.
        public bool CalledElevator { get; set; }
        // The start elevator shaft (when in elevator)
        public ElevatorShaft StartShaft { get; set; }
        // The target elevator shaft (when in elevator)
        public ElevatorShaft TargetShaft { get; set; }

        protected PathFinder _pathFinder;
        // The max time a guy can stay waiting.
        protected float _survivabilityTime;
        // The Elevator to enter.
        private Elevator _elevator;
        // Timer to check when to die.
        private float _deathTimer;
        // When where you when poppetje was kill?
        private bool _isDead;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="room">The room to spawn in.</param>
        /// <param name="survivability">The time it takes for people to die while waiting, people with -1 are invunerable.</param>
        /// <param name="walkingSpeed">The walking speed of people.</param>
        public Person(Room room) : base()
        {
            Sprite.LoadSprite("Guest");
            Sprite.DrawOrder = 1;

            if (Sprite.Texture != null)
                Sprite.SetSize(new Point(Sprite.Texture.Width, Sprite.Texture.Height));

            // Set the position in the center of the starting room.
            Position = new Vector2(room.Position.X + (room.RoomSize.X * (Room.ROOMWIDTH / 2)), room.Position.Y - (Room.ROOMHEIGHT - Sprite.DrawDestination.Height) - (Room.ROOMHEIGHT * (room.RoomSize.Y - 1)));

            // Set jump height (for walking)
            JumpHeight = 4;

            // Set the current room.
            CurrentRoom = room;

            CalledElevator = false;
            _isDead = false;

            // Load settings from the config.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();
            _survivabilityTime = config.Survivability;
            WalkingSpeed = config.WalkingSpeed * Room.ROOMWIDTH;

            _pathFinder = new PathFinder();
            Path = new List<Room>();

            // Start by moving to the center.
            CurrentTask = PersonTask.MovingCenter;
            Inside = false;
        }

        /// <summary>
        /// Called when person is arrives on his location.
        /// </summary>
        public virtual void OnArrival()
        {
            if (_roomBehaviour != null)
                _roomBehaviour.OnArrival(CurrentRoom, this);
        }

        /// <summary>
        /// Called when person leaves his location.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnDeparture()
        {
            if (_roomBehaviour != null && !Evacuating)
                _roomBehaviour.OnDeparture(CurrentRoom, this);
        }

        /// <summary>
        /// Call this to remove this object from the game.
        /// </summary>
        public override void Remove()
        {
            base.Remove();
        }

        /// <summary>
        /// Call this to kill the person.
        /// </summary>
        public virtual void Death()
        {
            // Set mode to dead
            _isDead = true;

            // Remove this object from the game.
            Remove();
        }

        /// <summary>
        /// Find a path to a room, using a rule. If a room has been found a path is saved and the target room is set.
        /// </summary>
        /// <param name="rule">The rule that is used for searching a room.</param>
        public void FindAndTargetRoom(FindPath rule)
        {/*
            // If the person is currently in the elevator, its current room is the targeted shaft.
            if (_targetShaft != null)
            {
                if (_startShaft != CurrentRoom)
                    CurrentRoom = _targetShaft;
            }*/

            // Set the bool if the elevator is allowed to be used.
            _pathFinder.UseElevator = !Evacuating;

            // Find the path to the target room.
            Path = _pathFinder.Find(CurrentRoom, rule);

            // Do some elevator shiz.
            if (CalledElevator)
            {
                if (TargetShaft != null)
                {
                    TargetShaft.ElevatorArrival -= TargetShaft_ElevatorArrival;
                    _elevator = null;
                    TargetShaft = null;
                    StartShaft.ElevatorArrival -= Person_ElevatorArrival;
                    StartShaft = null;
                }
                CalledElevator = false;
            }

            // Do stuff while there is a path.
            if (Path != null)
            {
                // The target room is the last room in the path.
                TargetRoom = Path.Last();

                // Get out of the room if the person is inside the room.
                if (Inside)
                {
                    Inside = false;
                    CurrentRoom.PeopleCount--;

                    // Call the departure event.
                    OnDeparture();
                }

                // Move this person to the center of the room.
                CurrentTask = PersonTask.MovingCenter;
            }
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public override void Update(float deltaTime)
        {
            if (!_isDead)
            {
                if (_deathTimer > _survivabilityTime && _survivabilityTime != -1)
                    Death();

                // While people are waiting, increase the deathtimer.
                if (CurrentTask == PersonTask.InQueue && _survivabilityTime != -1)
                {
                    _deathTimer += deltaTime;
                }

                // Move around.
                Move(deltaTime);
            }
            else
            {
                if (CurrentRoom.State != RoomState.Emergency || CurrentRoom.State != RoomState.InCleaning)
                    Death();
            }

            // Get the new bounding box (the exact position on the sprite batch)
            BoundingBox = Sprite.DrawDestination;
        }

        /// <summary>
        /// Move the person around.
        /// </summary>
        /// <param name="deltaTime"></param>
        private void Move(float deltaTime)
        {
            // If person is in the elevator, set its position to the elevator.
            if (_elevator != null)
            {
                Position = new Vector2(_elevator.Position.X + (_elevator.Sprite.DrawDestination.Width / 2 - Sprite.DrawDestination.Width / 2), _elevator.Position.Y - Room.ROOMHEIGHT + Sprite.DrawDestination.Height);
                CurrentTask = PersonTask.Waiting;
                return;
            } 

            Vector2 direction;

            // TODO: do this outside person
            // TODO: persons should not be dependant on sprite size, but on drawdestination.
            // Do moving in the room.
            switch (CurrentTask)
            {
                //When the person is waiting, do nothing.
                case PersonTask.Waiting:
                    break;
                #region movement code
                // When person is moving left.
                case PersonTask.MovingLeft:
                    Position = new Vector2(Position.X - WalkingSpeed * deltaTime, Position.Y);

                    if (Position.X < CurrentRoom.Position.X - Sprite.DrawDestination.Width)
                    {
                        Position = new Vector2(CurrentRoom.Position.X - Sprite.DrawDestination.Width, Position.Y);
                        CurrentRoom = CurrentRoom.Neighbors[Direction.West];
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;

                // When person is moving right.
                case PersonTask.MovingRight:
                    Position = new Vector2(Position.X + WalkingSpeed * deltaTime, Position.Y);

                    if (Position.X > CurrentRoom.Position.X + CurrentRoom.RoomSize.Y * Room.ROOMWIDTH)
                    {
                        Position = new Vector2(CurrentRoom.Position.X + CurrentRoom.RoomSize.Y * Room.ROOMWIDTH, Position.Y);
                        CurrentRoom = CurrentRoom.Neighbors[Direction.East];
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;

                // When person is moving up.
                case PersonTask.MovingUp:
                    Position = new Vector2(Position.X, Position.Y + WalkingSpeed * deltaTime);

                    if (Position.Y > CurrentRoom.Position.Y + Sprite.DrawDestination.Height)
                    {
                        Position = new Vector2(Position.X, CurrentRoom.Position.Y + Sprite.DrawDestination.Height);
                        CurrentRoom = CurrentRoom.Neighbors[Direction.North];
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;

                // When person is moving down.
                case PersonTask.MovingDown:
                    Position = new Vector2(Position.X, Position.Y - WalkingSpeed * deltaTime);

                    if (Position.Y < CurrentRoom.Position.Y - Room.ROOMHEIGHT - (Room.ROOMHEIGHT - Sprite.DrawDestination.Height))
                    {
                        Position = new Vector2(Position.X, CurrentRoom.Position.Y - Room.ROOMHEIGHT - (Room.ROOMHEIGHT - Sprite.DrawDestination.Height));
                        CurrentRoom = CurrentRoom.Neighbors[Direction.South];
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;
                #endregion 

                // When person is moving to the center of the room.
                case PersonTask.MovingCenter:

                    // walk to the center.
                    if (Position.X < CurrentRoom.Position.X + (CurrentRoom.RoomSize.X * Room.ROOMWIDTH / 2))
                        Position = new Vector2(Position.X + WalkingSpeed * deltaTime, Position.Y);
                    else
                        Position = new Vector2(Position.X - WalkingSpeed * deltaTime, Position.Y);

                    // If the person reached the center of the room.
                    if (Math.Abs(Position.X - CurrentRoom.Position.X - (CurrentRoom.RoomSize.X * Room.ROOMWIDTH / 2)) < WalkingSpeed * deltaTime)
                    {
                        Position = new Vector2(CurrentRoom.Position.X + (CurrentRoom.RoomSize.X * Room.ROOMWIDTH / 2), Position.Y);

                        // Get new room behaviour from this room.
                        _roomBehaviour = CurrentRoom.RoomBehaviour;

                        // If its not null, execute the room behaviour.
                        if (_roomBehaviour != null)
                            _roomBehaviour.OnPassRoom(CurrentRoom, this);

                        // Get a new task. (where to walk to next)
                        UpdateCurrentTask();

                        // When the task is still moving to the center (arrived), we wait.
                        if (CurrentTask == PersonTask.MovingCenter)
                            CurrentTask = PersonTask.Waiting;

                        // We the person reached the target room.
                        if (TargetRoom == CurrentRoom)
                        {
                            OnArrival();
                            TargetRoom = null;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Called when the elevator reaches the target shaft (for exiting the elevator)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetShaft_ElevatorArrival(object sender, EventArgs e)
        {
            // Check if this guest is not dead
            if (!_isDead)
            {
                // Set the room this person is in to the elevator this shaft.
                CurrentRoom = TargetShaft;

                // Unsubscribe from the event.
                TargetShaft.ElevatorArrival -= TargetShaft_ElevatorArrival;

                // Round off the position of the guest.
                Position = new Vector2(Position.X, _elevator.Position.Y - Room.ROOMHEIGHT + Sprite.Texture.Height);

                // Remove shaft info.
                TargetShaft = null;
                _elevator = null;
                CalledElevator = false;

                // Retarget the target room.
                FindAndTargetRoom(x => x == TargetRoom);

                // Move out of the elevator.
                CurrentTask = PersonTask.MovingCenter;
            }
        }

        /// <summary>
        /// Called when the elevator arrives on the floor. (for stepping into the elevator)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Person_ElevatorArrival(object sender, EventArgs e)
        {
            if (!_isDead)
            {
                _elevator = sender as Elevator;
                _elevator.CallElevator(TargetShaft.RoomPosition.Y, ElevatorDirection.Both);
                StartShaft.ElevatorArrival -= Person_ElevatorArrival;
                TargetShaft.ElevatorArrival += TargetShaft_ElevatorArrival;
            }
        }

        /// <summary>
        /// Updates the current task property to match the task that the person is doing now. (Used for path finding)
        /// </summary>
        private void UpdateCurrentTask()
        {
            if (Path == null)
            {
                CurrentTask = PersonTask.Waiting;
                return;
            }

            // If there are more rooms to go through
            if (Path.Count > 0)
            {
                // If the currently vistited room 
                if (CurrentRoom == Path[0])
                    Path.RemoveAt(0);

                // If there are still some more rooms to go through, and the next room in line is a neighbor.
                if (Path.Count > 0 && CurrentRoom.Neighbors.Values.Contains(Path[0]))
                {
                    // Get the direction of the neighbor.
                    Direction dir = Direction.None;
                    foreach (KeyValuePair<Direction, Room> kvp in CurrentRoom.Neighbors)
                    {
                        if (kvp.Value == Path[0])
                            dir = kvp.Key;
                    }

                    if (CurrentRoom is ElevatorShaft && Path[0] is ElevatorShaft)
                    {
                        return;
                    }


                    // Choose the action to take based on the direction.
                    switch (dir)
                    {
                        case Direction.None:
                            break;
                        case Direction.North:
                            CurrentTask = PersonTask.MovingUp;
                            break;
                        case Direction.East:
                            CurrentTask = PersonTask.MovingRight;
                            break;
                        case Direction.South:
                            CurrentTask = PersonTask.MovingDown;
                            break;
                        case Direction.West:
                            CurrentTask = PersonTask.MovingLeft;
                            break;
                        default:
                            break;
                    }
                }
            }
            // If there is nothing left to do, move towards the center.
            else
            {
                CurrentTask = PersonTask.MovingCenter;
            }
        }

        /// <summary>
        /// Draws the object onto the given sprite batch.
        /// </summary>
        /// <param name="batch">The sprite batch to draw to.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (!Inside)
            {
                // Make the person jump while moving.
                Sprite.SetPosition(new Point((int)Position.X, (int)(Position.Y + (JumpHeight / 2)) + (int)(Math.Sin(Position.X / 5) * JumpHeight)));

                base.Draw(batch, gameTime);
            }
        }

        public override string ToString()
        {
            string returnString = $"{Name};In Room: {CurrentRoom.Name}{Environment.NewLine}";

            if (TargetRoom != null)
                returnString += $"Target: {TargetRoom.Name}{Environment.NewLine}";

            //returnString += $"Task: {CurrentTask}{Environment.NewLine}Life: {_deathTimer}/{_survivabilityTime}";

            return returnString;
        }
    }
}
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

        public bool Inside { get; set; }

        private Room _targetRoom;
        public Room TargetRoom
        {
            get
            {
                return _targetRoom;
            }
            private set
            {
                _targetRoom = value;
            }
        }

        public List<Room> Path { get; protected set; }
        public float JumpHeight { get; set; }

        protected PathFinder _pathFinder;
        // The max time a guy can stay waiting.
        protected float _survivabilityTime;

        // The Elevator to enter.
        private Elevator _elevator;
        // The target elevator shaft (when in elevator)
        private ElevatorShaft _targetShaft;
        // The start elevator shaft (when in elevator)
        private ElevatorShaft _startStaft;
        // If this person already called the elevator on this floor.
        private bool _calledElevator;
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
            Sprite.SetSize(new Point(Sprite.Texture.Width, Sprite.Texture.Height));

            // Set the position in the center of the starting room.
            Position = new Vector2(room.Position.X + (room.RoomSize.X * (Room.ROOMWIDTH / 2)), room.Position.Y - (Room.ROOMHEIGHT - Sprite.Texture.Height) - (Room.ROOMHEIGHT * (room.RoomSize.Y - 1)));

            // Set jump height (for walking)
            JumpHeight = 4;

            // Set the current room.
            CurrentRoom = room;

            _calledElevator = false;
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
            if(_roomBehaviour != null)
                _roomBehaviour.OnArrival(CurrentRoom, this);
        }

        /// <summary>
        /// Called when person leaves his location.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnDeparture()
        {
            if(_roomBehaviour != null)
                _roomBehaviour.OnDeparture(CurrentRoom, this);
        }

        public override void Remove(EventArgs e)
        {
            // Set an emergency in this room.
            if (CurrentRoom.State != RoomState.Emergency && CurrentRoom.State != RoomState.InCleaning)
                CurrentRoom.SetEmergency(8);

            // Set mode to dead
            _isDead = true;

            base.Remove(e);
        }

        /// <summary>
        /// Find a path to a room, using a rule. If a room has been found a path is saved and the target room is set.
        /// </summary>
        /// <param name="rule">The rule that is used for searching a room.</param>
        public void FindAndTargetRoom(FindPath rule)
        {
            // If the person is currently in the elevator, its current room is the targeted shaft.
            if(_targetShaft != null)
            {
                if(_startStaft != CurrentRoom)
                    CurrentRoom = _targetShaft;
            }

            // Find the rooms, and its path
            Path = _pathFinder.Find(CurrentRoom, rule);

            if (_calledElevator)
            {
                if (_targetShaft != null)
                {
                    _targetShaft.ElevatorArrival -= TargetShaft_ElevatorArrival;
                    _elevator = null;
                    _targetShaft = null;
                    _startStaft.ElevatorArrival -= Person_ElevatorArrival;
                    _startStaft = null;
                }
                _calledElevator = false;
            }

            if (Path != null)
            {
                TargetRoom = Path.Last();

                if (Inside)
                {
                    OnDeparture();

                    CurrentRoom.PeopleCount--;
                    Inside = false;
                }

                CurrentTask = PersonTask.MovingCenter;
            }
        }

        /// <summary>
        /// Sets the current room of the person.
        /// </summary>
        /// <param name="room">The room to move to.</param>
        private void MoveToRoom(Room room)
        {
            CurrentRoom = room;
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
                    Remove(new EventArgs());

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
                if(CurrentRoom.State != RoomState.Emergency || CurrentRoom.State != RoomState.InCleaning)
                    Remove(new EventArgs());
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
                Position = new Vector2(_elevator.Position.X + (_elevator.Sprite.Texture.Width / 2 - Sprite.Texture.Width / 2), _elevator.Position.Y - Room.ROOMHEIGHT + Sprite.Texture.Height);
                CurrentTask = PersonTask.Waiting;
                return;
            }

            // TODO: do this outside person
            // TODO: make person 'snap' to their desination so that low framerates not break everything.
            // Do moving in the room.
            switch (CurrentTask)
            {
                //When the person is waiting, do nothing.
                case PersonTask.Waiting:
                    break;

                // When person is moving left.
                case PersonTask.MovingLeft:
                    Position = new Vector2(Position.X - WalkingSpeed * deltaTime, Position.Y);

                    if (Position.X < CurrentRoom.Position.X - Sprite.Texture.Width)
                    {
                        MoveToRoom(CurrentRoom.Neighbors[Direction.West]);
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;

                // When person is moving right.
                case PersonTask.MovingRight:
                    Position = new Vector2(Position.X + WalkingSpeed * deltaTime, Position.Y);

                    if (Position.X > CurrentRoom.Position.X + CurrentRoom.RoomSize.Y * Room.ROOMWIDTH)
                    {
                        MoveToRoom(CurrentRoom.Neighbors[Direction.East]);
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;

                // When person is moving up.
                case PersonTask.MovingUp:
                    Position = new Vector2(Position.X, Position.Y + WalkingSpeed * deltaTime);

                    if (Position.Y > CurrentRoom.Position.Y + Sprite.Texture.Height)
                    {
                        MoveToRoom(CurrentRoom.Neighbors[Direction.North]);
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;

                // When person is moving down.
                case PersonTask.MovingDown:
                    Position = new Vector2(Position.X, Position.Y - WalkingSpeed * deltaTime);

                    if (Position.Y < CurrentRoom.Position.Y - Room.ROOMHEIGHT - (Room.ROOMHEIGHT - Sprite.Texture.Height))
                    {
                        MoveToRoom(CurrentRoom.Neighbors[Direction.South]);
                        CurrentTask = PersonTask.MovingCenter;
                    }

                    break;

                // When person is moving to the center of the room.
                case PersonTask.MovingCenter:

                    if (Position.X < CurrentRoom.Position.X + (CurrentRoom.RoomSize.X * Room.ROOMWIDTH / 2))
                        Position = new Vector2(Position.X + WalkingSpeed * deltaTime, Position.Y);
                    else
                        Position = new Vector2(Position.X - WalkingSpeed * deltaTime, Position.Y);

                    // If the person reached the center of the room.
                    if (Math.Abs(Position.X - CurrentRoom.Position.X - (CurrentRoom.RoomSize.X * Room.ROOMWIDTH / 2)) < WalkingSpeed * deltaTime)
                    {
                        _roomBehaviour = CurrentRoom.roomBehaviour;

                        if(_roomBehaviour != null)
                            _roomBehaviour.OnPassRoom(CurrentRoom, this);

                        // Get a new task.
                        UpdateCurrentTask();

                        // When the task is still moving to the center, we wait.
                        if (CurrentTask == PersonTask.MovingCenter)
                            CurrentTask = PersonTask.Waiting;

                        if (TargetRoom == CurrentRoom)
                        {
                            OnArrival();
                            TargetRoom = null;
                        }

                        // TODO: DO THIS IN ROOM BEHAVIOUR!!!!
                        // If the elevator is not yet called, and the person is in an elevatorshaft.
                        if (!_calledElevator && CurrentRoom is ElevatorShaft && Path != null && Path.Count > 1 && Path.ElementAt(0) is ElevatorShaft)
                        {
                            _startStaft = CurrentRoom as ElevatorShaft;
                            // Get the last elevator in the path (CAN BREAK WITH MULTIPLE ELEVATORS!)
                            _targetShaft = Path.OfType<ElevatorShaft>().LastOrDefault();

                            if (_targetShaft != null)
                            {
                                // Call the elevator
                                _calledElevator = true;
                                (CurrentRoom as ElevatorShaft).CallElevator(_targetShaft.RoomPosition.Y);
                                (CurrentRoom as ElevatorShaft).ElevatorArrival += Person_ElevatorArrival;
                                CurrentTask = PersonTask.InQueue;
                            }
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
            if (!_isDead)
            {
                MoveToRoom(_targetShaft);
                _targetShaft.ElevatorArrival -= TargetShaft_ElevatorArrival;

                Position = new Vector2(Position.X, _elevator.Position.Y - Room.ROOMHEIGHT + Sprite.Texture.Height);

                _targetShaft = null;
                _elevator = null;
                _calledElevator = false;

                FindAndTargetRoom(x => x == TargetRoom);

                // Move out from the elevator.
                CurrentTask = PersonTask.MovingCenter;
            }
        }

        /// <summary>
        /// Called when the elevator arrives on the floor. (for stepping into the elevator)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Person_ElevatorArrival(object sender, EventArgs e)
        {
            if(!_isDead)
            {
                _elevator = sender as Elevator;
                _startStaft.ElevatorArrival -= Person_ElevatorArrival;
                _targetShaft.ElevatorArrival += TargetShaft_ElevatorArrival;
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

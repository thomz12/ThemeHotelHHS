using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        MovingLeft,
        MovingRight,
        MovingUp,
        MovingDown,
        MovingCenter
    }

    public abstract class Person : HotelObject
    {
        public float WalkingSpeed { get; set; }
        public PersonTask CurrentTask { get; set; }
        public Room CurrentRoom { get; set; }

        private Room _targetRoom;
        public Room TargetRoom
        {
            get
            {
                return _targetRoom;
            }
            set
            {
                _targetRoom = value;
                Path = FindPath(_targetRoom);
                GetCurrentTask();
            }
        }

        public List<Room> Path { get; set; }
        public float JumpHeight { get; set; }

        private bool _calledElevator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Person(ContentManager content, Room room) : base(content)
        {
            Sprite.LoadSprite("Guest");
            Sprite.DrawOrder = 1;
            Sprite.SetSize(new Point(Sprite.Texture.Width, Sprite.Texture.Height));
            Position = new Vector2(room.Position.X, room.Position.Y - (Room.ROOMHEIGHT - Sprite.Texture.Height));
            JumpHeight = 4;
            CurrentRoom = room;
            _calledElevator = false;

            Path = new List<Room>();
            room.PeopleCount++;
            WalkingSpeed = 50.0f;

            CurrentTask = PersonTask.Waiting;
        }

        public void MoveToRoom(Room room)
        {
            CurrentRoom.PeopleCount--;
            CurrentRoom = room;
            room.PeopleCount++;
        }

        public override void Update(float deltaTime)
        {
            // y-position (jumping)
            /*
            if (CurrentTask != PersonTask.MovingUp && CurrentTask != PersonTask.MovingDown && CurrentTask != PersonTask.Waiting)
                Position = new Vector2(Position.X, ((float)Math.Sin(Position.X) * JumpHeight + JumpHeight / 2) + CurrentRoom.Position.Y - (CurrentRoom.RoomSize.Y * Room.ROOMHEIGHT) + Sprite.Texture.Height);
            */
            Move(deltaTime);

            // Get the new bounding box (the exact position on the sprite batch)
            BoundingBox = Sprite.DrawDestination;
        }

        private void Move(float deltaTime)
        {
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

                    if (Math.Abs(Position.X - CurrentRoom.Position.X - (CurrentRoom.RoomSize.X * Room.ROOMWIDTH / 2)) < WalkingSpeed * deltaTime)
                    {
                        GetCurrentTask();

                        if (CurrentTask == PersonTask.MovingCenter)
                            CurrentTask = PersonTask.Waiting;
                    }
                    break;
                default:
                    break;
            }
        }

        private void GetCurrentTask()
        {
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

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            // Make the person jump while moving.
            Sprite.SetPosition( new Point((int)Position.X, (int)(Position.Y + (JumpHeight / 2))+ (int)(Math.Sin(Position.X) * JumpHeight)));

            base.Draw(batch, gameTime);
        }

        // Node used for finding the path.
        class RoomNode
        {
            public Room Room;
            public int Weight;
            public RoomNode PrevRoom;

            public RoomNode(Room room, int weight, RoomNode prevRoom)
            {
                Room = room;
                Weight = weight;
                PrevRoom = prevRoom;
            }
        }

        /// <summary>
        /// Finds the shortest path to a given room.
        /// </summary>
        /// <param name="targetRoom"></param>
        /// <param name="rooms"></param>
        /// <returns>The shortest path to take to the given room.</returns>
        public List<Room> FindPath(Room targetRoom)
        {
            List<RoomNode> queue = new List<RoomNode>();
            List<RoomNode> visited = new List<RoomNode>();
            RoomNode endNode = null;

            // Add the current room.
            queue.Add(new RoomNode(CurrentRoom, 0, null));

            while(queue.Count > 0)
            {
                RoomNode current = queue.OrderBy(x => x.Weight).First();

                if (current.Room == targetRoom)
                {
                    endNode = current;
                    break;
                }

                foreach(Room room in current.Room.Neighbors.Values)
                {
                    if(!visited.Select(x => x.Room).Contains(room))
                        queue.Add(new RoomNode(room, current.Weight + room.Weight, current));
                }

                visited.Add(current);
                queue.Remove(current);
            }

            // If no end node was found, return null.
            if (endNode == null)
                return null;

            // The path to follow
            List<RoomNode> path = new List<RoomNode>();
            // begin with the end node
            path.Add(endNode);
            // keep adding the previous node to the final path
            while(true)
            {
                // the previous node
                RoomNode node = path.Last().PrevRoom;

                if (node != null)
                    path.Add(node);
                else
                    break;
            }

            // reverse so that current room is start room, and end room is the last room.
            return path.Select(x => x.Room).Reverse().ToList();
        }

        public override string ToString()
        {
            return $"{Name};In Room: {CurrentRoom.Name}";
        }
    }
}

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
        MovingCenter
    }

    public abstract class Person : HotelObject
    {
        public float WalkingSpeed { get; set; }
        public PersonTask CurrentTask { get; set; }
        public Room CurrentRoom { get; set; }
        public List<Room> Path { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Person(ContentManager content, Room room) : base(content)
        {
            Sprite.LoadSprite("Guest");
            Sprite.DrawOrder = 1;
            Sprite.SetSize(new Point(25, 90));
            Position = new Vector2(room.Position.X, room.Position.Y);

            CurrentRoom = room;

            WalkingSpeed = 50.0f;

            CurrentTask = PersonTask.MovingCenter;
        }

        public override void Update(float deltaTime)
        {
            // Do moving in the room.
            switch (CurrentTask)
            {
                case PersonTask.Waiting:
                    break;
                case PersonTask.MovingLeft:
                    Position = new Vector2(Position.X - WalkingSpeed * deltaTime, Position.Y);
                    break;
                case PersonTask.MovingRight:
                    Position = new Vector2(Position.X + WalkingSpeed * deltaTime, Position.Y);
                    break;
                case PersonTask.MovingCenter:
                    if(Position.X < CurrentRoom.Position.X + (CurrentRoom.RoomSize.Y * Room.ROOMWIDTH / 2))
                        Position = new Vector2(Position.X + WalkingSpeed * deltaTime, Position.Y);
                    else
                        Position = new Vector2(Position.X - WalkingSpeed * deltaTime, Position.Y);

                    if(Math.Abs(Position.X - CurrentRoom.Position.X - (CurrentRoom.RoomSize.Y * Room.ROOMWIDTH / 2)) < WalkingSpeed * deltaTime)
                    {
                        CurrentTask = PersonTask.Waiting;
                    }
                    break;
                default:
                    break;
            }

            // Update sprite position
            Sprite.SetPosition(new Point((int)Position.X, (int)Position.Y));

            // Get the new bounding box (the exact position on the sprite batch)
            BoundingBox = Sprite.DrawDestination;
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
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
                    if(!visited.Contains(current))
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
    }
}

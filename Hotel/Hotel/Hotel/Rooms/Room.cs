using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public enum RoomState
    {
        None,
        Vacant,
        Dirty,
        InCleaning,
        Occupied,
        Emergency
    }

    public enum Direction
    {
        None,
        North,
        East,
        South,
        West
    }

    public abstract class Room : HotelObject
    {
        public const int ROOMHEIGHT = 90;
        public const int ROOMWIDTH = 192;

        public int ID { get; set; }

        public Point RoomPosition { get; set; }
        public Point RoomSize { get; set; }

        public bool Vertical { get; set; }
        public int PeopleCount { get; set; }
        public int PeopleInside { get; set; }
        public float Weight { get; set; }
        public RoomState State { get; set; }

        private Texture2D _emergencyTexture;
        private float _emergencyTime;

        public Dictionary<Direction, Room> Neighbors { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager.</param>
        /// <param name="ID">The id given to the room.</param>
        /// <param name="position">The position of the room.</param>
        /// <param name="size">The size of the room.</param>
        public Room(ContentManager content, int id, Point position, Point size) : base(content)
        {
            Sprite = new Sprite(content);
            ID = id;
            RoomPosition = position;
            RoomSize = size;
            Vertical = false;
            Weight = 1.0f * size.X;

            Neighbors = new Dictionary<Direction, Room>();

            // Set the 'real' position of the room.
            Position = new Vector2(position.X * ROOMWIDTH, position.Y * ROOMHEIGHT);

            _emergencyTexture = content.Load<Texture2D>("CleaningEmergency");

            Sprite.SetPosition(new Point((int)Position.X, (int)Position.Y));
            Sprite.SetSize(new Point(size.X * ROOMWIDTH, size.Y * ROOMHEIGHT));
            Sprite.DrawOrder = 0.1f;

            BoundingBox = Sprite.DrawDestination;
        }

        /// <summary>
        /// Reverses the given direction.
        /// </summary>
        /// <param name="dir">The direction to reverse.</param>
        /// <returns>The reversed direction.</returns>
        public Direction ReverseDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.None:
                    return Direction.None;
                case Direction.North:
                    return Direction.South;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                default:
                    return Direction.None;
            }
        }

        /// <summary>
        /// Check if the given room is a neighbor.
        /// </summary>
        /// <param name="roomToCheck">The room to check.</param>
        /// <returns>The direction of the room, None if it is not a neighbor.</returns>
        public Direction IsNeighbor(Room roomToCheck)
        {
            // Room rectangles.
            Rectangle room1rect = new Rectangle(RoomPosition.X, RoomPosition.Y, RoomSize.X, RoomSize.Y);
            Rectangle room2rect = new Rectangle(roomToCheck.RoomPosition.X, roomToCheck.RoomPosition.Y, roomToCheck.RoomSize.X, roomToCheck.RoomSize.Y);

            // Check for rooms on the same floor
            if (room1rect.Y - room1rect.Height == room2rect.Y - room2rect.Height)
            {
                if (room1rect.Left == room2rect.Right)
                    return Direction.West;
                if (room1rect.Right == room2rect.Left)
                    return Direction.East;
            }

            // Check for rooms above or below.
            if (Vertical)
            {
                if (room1rect.Left == room2rect.Left)
                {
                    if (room1rect.Top == room2rect.Bottom)
                        return Direction.South;
                    if (room1rect.Bottom == room2rect.Top)
                        return Direction.North;
                }
            }

            return Direction.None;
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (State == RoomState.Emergency)
            {
                _emergencyTime += deltaTime;
            }
            else
                _emergencyTime = 0;
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            base.Draw(batch, gameTime);

            // if cleaning emergency, show emergency sprite
            if (State == RoomState.Emergency)
            {
                if ((int)_emergencyTime % 2 == 0)
                    batch.Draw(_emergencyTexture, Sprite.DrawDestination, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);
            }
        }

        public override string ToString()
        {
            return $"{Name};Floor: {RoomPosition.Y}{Environment.NewLine}Size: {RoomSize.X}x{RoomSize.Y}{Environment.NewLine}People: {PeopleCount}";
        }
    }
}
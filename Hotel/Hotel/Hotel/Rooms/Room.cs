using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
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

        public Point RoomPosition { get; set; }
        public Point RoomSize { get; set; }

        public bool Vertical { get; set; }
        public int PeopleCount { get; set; }
        public int Weight { get; set; }

        public Dictionary<Direction, Room> Neighbors { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Room(ContentManager content, Point position, Point size) : base(content)
        {
            Sprite = new Sprite(content);
            RoomPosition = position;
            RoomSize = size;
            Vertical = false;

            Neighbors = new Dictionary<Direction, Room>();

            // Set the 'real' position of the room.
            Position = new Vector2(position.X * ROOMWIDTH, position.Y * ROOMHEIGHT);

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

        public override string ToString()
        {
            return $"{Name};Floor: {RoomPosition.Y}{Environment.NewLine}Size: {RoomSize.X}x{RoomSize.Y}{Environment.NewLine}People: {PeopleCount}";
        }
    }
}
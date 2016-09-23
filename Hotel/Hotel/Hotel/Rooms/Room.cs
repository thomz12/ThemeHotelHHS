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

    public abstract class Room
    {
        public const int ROOMHEIGHT = 90;
        public const int ROOMWIDTH = 192;

        public Sprite Sprite { get; private set; }

        public Point RoomPosition { get; set; }
        public Point RoomSize { get; set; }

        public bool Vertical { get; set; }

        public int Weight { get; set; }

        public Dictionary<Direction, Room> Neighbors { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Room(ContentManager content, Point position, Point size)
        {
            Sprite = new Sprite(content);
            RoomPosition = position;
            RoomSize = size;
            Vertical = false;

            Neighbors = new Dictionary<Direction, Room>();

            Sprite.SetPosition(new Point(position.X * ROOMWIDTH, position.Y * ROOMHEIGHT));
            Sprite.SetSize(new Point(size.X * ROOMWIDTH, size.Y * ROOMHEIGHT));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="deltaTime">The game time.</param>
        public virtual void Update(float deltaTime)
        {
            Sprite.Update(deltaTime);
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Sprite.Draw(batch, gameTime);
        }
    }
}
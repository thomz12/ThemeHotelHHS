using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework;

namespace Hotel.Persons
{
    public class Cleaner : Person
    {
        private int _cleaningTimer;
        private int _cleaningDuration;
        private bool _isCleaning;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Cleaner(ContentManager content, Room room, float walkingSpeed, int cleaningDuration) : base(content, room, walkingSpeed)
        {
            Sprite.LoadSprite("Cleaner");
            _isCleaning = false;
            _cleaningDuration = cleaningDuration;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override string ToString()
        {
            return $"{Name};In Room: {CurrentRoom.Name}{Environment.NewLine}Target: {TargetRoom.Name}{Environment.NewLine}";
        }
    }
}

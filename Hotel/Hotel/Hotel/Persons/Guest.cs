﻿using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Hotel.Rooms;
using Microsoft.Xna.Framework;
using System.IO;

namespace Hotel.Persons
{
    public enum Gender
    {
        Male,
        Female,
        Genderless
    }

    public class Guest : Person
    {
        public Gender Gender { get; private set; }

        private GuestRoom _room;
        public GuestRoom Room
        {
            get
            {
                return _room;
            }
            set
            {
                _room = value;
                TargetRoom = _room;
            }
        }

        public int Classification { get; set; }

        private static Random _random = new Random();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Guest(ContentManager content, Room room, float walkingSpeed) : base(content, room, walkingSpeed)
        {
            NameGenerator generator = new NameGenerator();

            if (_random.Next(0, 2) == 0)
            {
                Gender = Gender.Male;
                Sprite.LoadSprite("Guest");
            }
            else
            {
                Gender = Gender.Female;
                Sprite.LoadSprite("FemaleGuest");
            }

            // Give this person a name.
            Name = generator.GenerateName(Gender);

            Arrival += Guest_Arrival;
        }

        // When the guest arrives
        private void Guest_Arrival(object sender, EventArgs e)
        {
            if(CurrentRoom is Lobby)
            {
                //
            }
        }

        public override string ToString()
        {
            string returnString = $"{Name};Gender: {Gender}{Environment.NewLine}In Room: {CurrentRoom.Name}{Environment.NewLine}";

            if (TargetRoom != null)
                returnString += $"Target: {TargetRoom.Name}{Environment.NewLine}";

            return returnString;
        }
    }
}

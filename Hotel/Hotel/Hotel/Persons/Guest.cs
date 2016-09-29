using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
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
        
        private static Random _random = new Random();
        private NameGenerator generator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content manager used to load in images.</param>
        public Guest(ContentManager content, Room room) : base(content, room)
        {
            generator = new NameGenerator();

            if (_random.Next(0, 2) == 0)
                Gender = Gender.Male;
            else
                Gender = Gender.Female;

            // Give this person a name.
            Name = generator.GenerateName(Gender);
        }

        public override string ToString()
        {
            return $"{Name};Gender: {Gender}{Environment.NewLine}In Room: {CurrentRoom.Name}{Environment.NewLine}Target: {TargetRoom.Name}{Environment.NewLine}";
        }
    }
}

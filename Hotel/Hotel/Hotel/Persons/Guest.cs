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

    public enum StayState
    {
        None,
        CheckIn,
        CheckOut,
        Staying,
    }

    public class Guest : Person
    {
        public Gender Gender { get; private set; }
        public GuestRoom Room { get; set; }
        public int Classification { get; set; }
        public StayState StayState { get; set; }

        private float _roomTime;
        private bool _toLeave;

        private static Random _random = new Random();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="room">The room to spawn the guest in.</param>
        /// <param name="survivability">The time the guest can stand in a queue without dieing.</param>
        /// <param name="walkingSpeed">The speed at which the guest walks.</param>
        public Guest(Room room) : base(room)
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

            StayState = StayState.None;

            // Give this person a name.
            Name = generator.GenerateName(Gender);
        }

        public void LeaveRoomInTime(float seconds)
        {
            _roomTime = seconds;
            _toLeave = true;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // If person has to leave in time.
            if(_toLeave && Inside)
            {
                _roomTime -= deltaTime;
                // if the guest has no time left.
                if (_roomTime <= 0)
                {
                    Inside = false;
                    _toLeave = false;
                    OnDeparture();
                }
            }
        }

        /// <summary>
        /// Call this to remove this guest from the game.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        public override void Remove(EventArgs e)
        {
            if (StayState == StayState.Staying)
            {
                Room.Guest = null;
                StayState = StayState.None;
            }

            base.Remove(e);
        }
    }
}

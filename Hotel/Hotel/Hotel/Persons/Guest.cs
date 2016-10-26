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
        public Guest(Room room) : base(room)
        {
            // Create a new name generator.
            NameGenerator generator = new NameGenerator();

            // Make this guest a boy or a girl.
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

            // We dont have a staystate when this guest is spawned in.
            StayState = StayState.None;

            // Give this person a name.
            Name = generator.GenerateName(Gender);
        }

        /// <summary>
        /// Sets the time that a person is allowed to stay in this room, after it expires the OnDeparture() function is called.
        /// </summary>
        /// <param name="seconds">The amount of time to stay in the room.</param>
        public void SetTimeToStayInRoom(float seconds)
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

            // If you are outside and your staystate is CheckOut; go RIP, but dont leave a mess. 
            if (CurrentRoom.Name.Equals("Outside") && StayState == StayState.CheckOut)
                this.Remove();
        }

        /// <summary>
        /// Call this to kill the person.
        /// </summary>
        public override void Death()
        {
            // Set the staying state to none and empty the occupied room
            if (StayState == StayState.Staying)
            {
                Room.Guest = null;
                StayState = StayState.None;
            }

            // Set an emergency in this room.
            if (CurrentRoom.State != RoomState.Emergency && CurrentRoom.State != RoomState.InCleaning)
                CurrentRoom.SetEmergency(8);

            base.Death();
        }
    }
}

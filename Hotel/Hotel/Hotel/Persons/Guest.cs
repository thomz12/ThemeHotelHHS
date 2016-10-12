﻿using System;
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
            }
        }

        public StayState StayState { get; set; }

        public int Classification { get; set; }

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

            // Subscribe to some events
            Arrival += Guest_Arrival;
            Death += Guest_Death;
        }

        private void Guest_Death(object sender, EventArgs e)
        {
            if(StayState == StayState.Staying)
            {
                Room.Guest = null;
                StayState = StayState.None;
            }
        }

        // When the guest arrives
        private void Guest_Arrival(object sender, EventArgs e)
        {
            if(CurrentRoom is Lobby)
            {
                if(StayState == StayState.CheckIn)
                    (CurrentRoom as Lobby).CheckIn(this);
                else if(StayState == StayState.CheckOut)
                    (CurrentRoom as Lobby).CheckOut(this);
            }

            if(CurrentRoom is GuestRoom)
            {
                if (CurrentRoom == Room)
                {
                    Room.PeopleCount++;
                    Inside = true;
                }
            }

            if(CurrentRoom is Cinema)
            {
                Cinema cinema = CurrentRoom as Cinema;

                if(cinema.Open)
                {
                    cinema.PeopleCount++;
                    cinema.Finished += Cinema_Finished;
                    Inside = true;
                }
                else
                {
                    FindAndTargetRoom(x => x == Room);
                }
            }
        }

        private void Cinema_Finished(object sender, EventArgs e)
        {
            (CurrentRoom as Cinema).Finished -= Cinema_Finished;
            FindAndTargetRoom(x => x == Room);
        }

        public void CheckOut(Lobby lobby)
        {
            FindAndTargetRoom(x => x is Lobby && (x as Lobby).Receptionist != null);
            StayState = StayState.CheckOut;
        }
    }
}

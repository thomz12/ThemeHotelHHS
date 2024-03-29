﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;
using Hotel.Persons;

namespace Hotel.RoomBehaviours
{
    class CinemaBehaviour : IRoomBehaviour
    {
        public void OnArrival(Room room, Person person)
        {
            Cinema cinema = room as Cinema;

            if (person is Guest)
            {
                if (cinema.Open)
                {
                    person.Inside = true;
                    cinema.PeopleCount++;
                }
                else
                {
                    person.FindAndTargetRoom(x => x == (person as Guest).Room);
                }
            }
        }

        public void OnDeparture(Room room, Person person)
        {
            if (person is Guest)
            {
                person.FindAndTargetRoom(x => x == (person as Guest).Room);
            }
        }

        public void OnPassRoom(Room room, Person person)
        {
            return;
        }
    }
}

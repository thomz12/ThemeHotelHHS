using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Persons;

namespace Hotel
{
    class FitnessBehaviour : IRoomBehaviour
    {
        public void OnArrival(Room room, Person person)
        {
            if (person is Guest)
            {
                person.Inside = true;
                room.PeopleCount++;
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

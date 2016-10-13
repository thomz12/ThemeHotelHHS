using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;
using Hotel.Persons;

namespace Hotel
{
    class CafeBehaviour : IRoomBehaviour
    {
        public void OnArrival(Room room, Person person)
        {
            Cafe cafe = room as Cafe;

            if (person is Guest && cafe.PeopleCount < cafe.Capacity)
            {
                person.Inside = true;
                cafe.PeopleCount++;
            }
        }

        public void OnDeparture(Room room, Person person)
        {
            if (person is Guest && person.Inside)
            {
                room.PeopleCount--;
                person.Inside = false;
                person.FindAndTargetRoom(x => x == (person as Guest).Room);
            }
        }

        public void OnPassRoom(Room room, Person person)
        {
            return;
        }

        public void RoomUpdate(Room room, Person person)
        {
            return;
        }
    }
}

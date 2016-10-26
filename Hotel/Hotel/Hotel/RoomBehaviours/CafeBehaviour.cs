using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;
using Hotel.Persons;

namespace Hotel.RoomBehaviours
{
    class CafeBehaviour : IRoomBehaviour
    {
        public void OnArrival(Room room, Person person)
        {
            Cafe cafe = room as Cafe;

            if (person is Guest && cafe.PeopleCount < cafe.Capacity)
            {
                person.Inside = true;
                (person as Guest).LeaveRoomInTime(5);
                cafe.PeopleCount++;
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

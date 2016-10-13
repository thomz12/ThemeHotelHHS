using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Persons;

namespace Hotel
{
    class GuestRoomBehaviour : IRoomBehaviour
    {
        public void OnArrival(Room room, Person person)
        {
            GuestRoom guestRoom = room as GuestRoom;

            if (person is Guest)
            {
                Guest guest = person as Guest;

                if (guest.Room == room)
                {
                    room.PeopleCount++;
                    person.Inside = true;
                }
            }
        }

        public void OnDeparture(Room room, Person person)
        {
            if (person is Guest)
            {
                room.PeopleCount--;
                person.Inside = false;
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

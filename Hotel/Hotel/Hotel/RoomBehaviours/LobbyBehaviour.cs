using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Persons;
using Hotel.Rooms;

namespace Hotel.RoomBehaviours
{
    class LobbyBehaviour : IRoomBehaviour
    {
        public void OnArrival(Room room, Person person)
        {
            Lobby lobby = room as Lobby;

            if (person is Guest)
            {
                Guest guest = person as Guest;

                if (guest.StayState == StayState.CheckIn)
                    lobby.CheckIn(guest);

                if (guest.StayState == StayState.CheckOut)
                    lobby.CheckOut(guest);
            }
        }

        public void OnDeparture(Room room, Person person)
        {
            return;
        }

        public void OnPassRoom(Room room, Person person)
        {
            return;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    /// <summary>
    /// Used for implementing behaviour for the guest.
    /// </summary>
    public interface IRoomBehaviour
    {
        void OnArrival(Room room, Persons.Person person);
        void OnDeparture(Room room, Persons.Person person);
        void OnPassRoom(Room room, Persons.Person person);
    }
}

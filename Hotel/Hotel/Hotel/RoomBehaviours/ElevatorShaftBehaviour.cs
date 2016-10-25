using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Persons;

namespace Hotel.RoomBehaviours
{
    public class ElevatorShaftBehaviour : IRoomBehaviour
    {
        public void OnArrival(Room room, Person person)
        {
            return;
        }

        public void OnDeparture(Room room, Person person)
        {
            return;
        }

        public void OnPassRoom(Room room, Person person)
        {
            if (!person._calledElevator && person.CurrentRoom is ElevatorShaft && person.Path != null && person.Path.Count > 1 && person.Path.ElementAt(0) is ElevatorShaft)
            {
                person._startShaft = person.CurrentRoom as ElevatorShaft;

                person._targetShaft = person._startShaft;

                for (int i = 0; i < i + 1; i++)
                {
                    if (!(person.Path.ElementAt(i) is ElevatorShaft))
                        break;
                    person._targetShaft = person.Path.ElementAt(i) as ElevatorShaft;
                }

                if (person._targetShaft != null && person._targetShaft != person._startShaft)
                {
                    // Call the elevator
                    person._calledElevator = true;
                    (person.CurrentRoom as ElevatorShaft).CallElevator(person._targetShaft.RoomPosition.Y);
                    (person.CurrentRoom as ElevatorShaft).ElevatorArrival += person.Person_ElevatorArrival;
                    person.CurrentTask = PersonTask.InQueue;
                }
            }
        }
    }
}

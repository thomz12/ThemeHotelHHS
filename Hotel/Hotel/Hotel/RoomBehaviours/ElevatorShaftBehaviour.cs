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
            // Make sure the person has not called the elvator yet, and wants to call the elevator.
            if (!person._calledElevator && person.CurrentRoom is ElevatorShaft && person.Path != null && person.Path.Count > 1 && person.Path.ElementAt(0) is ElevatorShaft)
            {
                // Get the start shaft (the shaft to begin the elevator trip).
                person._startShaft = person.CurrentRoom as ElevatorShaft;

                // Set the end shaft to the start shaft(the shaft to end the elvator trip).
                person._targetShaft = person._startShaft;

                // Find the real end shaft.
                for (int i = 0; i < person.Path.Count; i++)
                {
                    if (!(person.Path.ElementAt(i) is ElevatorShaft))
                        break;
                    person._targetShaft = person.Path.ElementAt(i) as ElevatorShaft;
                }

                // If we have a target shaft that's not a start shaft, this person calls the elevator!
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

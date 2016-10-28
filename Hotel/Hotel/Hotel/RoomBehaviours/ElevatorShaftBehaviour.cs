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
            // TODO _calledElevator is private??!!
            if (!person.CalledElevator && person.CurrentRoom is ElevatorShaft && person.Path != null && person.Path.Count > 1 && person.Path.ElementAt(0) is ElevatorShaft)
            {
                // Get the start shaft (the shaft to begin the elevator trip).
                person.StartShaft = person.CurrentRoom as ElevatorShaft;

                // Set the end shaft to the start shaft(the shaft to end the elvator trip).
                person.TargetShaft = person.StartShaft;

                // Find the real end shaft.
                for (int i = 0; i < person.Path.Count; i++)
                {
                    if (!(person.Path.ElementAt(i) is ElevatorShaft))
                        break;
                    person.TargetShaft = person.Path.ElementAt(i) as ElevatorShaft;
                }

                ElevatorDirection dir = person.TargetShaft.RoomPosition.Y < person.StartShaft.RoomPosition.Y ? ElevatorDirection.Down : ElevatorDirection.Up;

                // If we have a target shaft that's not a start shaft, this person calls the elevator!
                if (person.TargetShaft != null && person.TargetShaft != person.StartShaft)
                {
                    // Call the elevator
                    person.CalledElevator = true;
                    (person.CurrentRoom as ElevatorShaft).CallElevator(dir);
                    (person.CurrentRoom as ElevatorShaft).ElevatorArrival += person.Person_ElevatorArrival;
                    person.CurrentTask = PersonTask.InQueue;
                }
            }
        }
    }
}

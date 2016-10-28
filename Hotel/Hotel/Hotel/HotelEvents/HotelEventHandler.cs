using Hotel.Persons;
using Hotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HotelEvents;

namespace Hotel
{
    public class HotelEventHandler
    {
        private Hotel _hotel;

        public HotelEventHandler(Hotel hotel)
        {
            _hotel = hotel;
        }

        public void HandleEvent(HotelEvent evt)
        {
            switch (evt.EventType)
            {
                // The 'NONE' event.
                case HotelEventType.NONE:
                    break;

                // Check-in event.
                case HotelEventType.CHECK_IN:
                    _hotel.AddGuest(evt.Data.Keys.ElementAt(0), Int32.Parse(Regex.Replace(evt.Data.Values.ElementAt(0), "[^0-9]+", string.Empty)));
                    break;

                // Check-out event.
                case HotelEventType.CHECK_OUT:
                    // Get the guest that needs to be checked out.
                    string objectName = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);
                    if (_hotel.Guests.Keys.Contains(objectName))
                    {
                        Guest aGuest = (Guest)_hotel.Guests[objectName];
                        aGuest.FindAndTargetRoom(x => x is Lobby && (x as Lobby).Receptionist != null);
                        aGuest.StayState = StayState.CheckOut;
                    }
                    break;

                // Cleaning Emergency event.
                case HotelEventType.CLEANING_EMERGENCY:
                    // Get the specific room that has an emergency
                    Room roomWithEmergency = _hotel.Rooms.Where(x => x.ID == Int32.Parse(evt.Data.Values.ElementAt(0))).FirstOrDefault();
                    // Check if the room exists.
                    if (roomWithEmergency != null)
                        roomWithEmergency.SetEmergency(float.Parse(evt.Data.Values.ElementAt(1)));
                    break;

                // Evecuation event.
                case HotelEventType.EVACUATE:

                    _hotel.Evacuating = true;

                    // Send the elevator to the first floor.
                    ElevatorShaft shaft = (ElevatorShaft)_hotel.Rooms.Where(x => x is ElevatorShaft && x.RoomPosition.Y == 0).FirstOrDefault();
                    if (shaft != null)
                    {
                        if (shaft.Elevator.CurrentFloor != 0)
                            shaft.Elevator.CallElevator(0, 0);
                    }

                    // Send the guests outside.
                    foreach (Guest g in _hotel.Guests.Values)
                    {
                        g.Evacuating = true;
                        g.FindAndTargetRoom(x => x.Name == "Outside");
                    }

                    // Send the staff outside.
                    foreach (Person staff in _hotel.Staff)
                    {
                        staff.Evacuating = true;
                        staff.FindAndTargetRoom(x => x.Name == "Outside");
                    }

                    break;

                // Godzilla event.
                case HotelEventType.GODZILLA:
                    Console.WriteLine("AAAAAAAHHHHHHHH!");
                    // Evacuate all the people?!

                    _hotel.Evacuating = true;

                    break;

                // Guest needs food event.
                case HotelEventType.NEED_FOOD:

                    string guestName = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);
                    if (_hotel.Guests.Keys.Contains(guestName))
                    {
                        Guest hotelGuest = _hotel.Guests[guestName] as Guest;

                        if (hotelGuest.StayState == StayState.Staying && !_hotel.Evacuating)
                            hotelGuest.FindAndTargetRoom(x => x is Cafe);
                    }

                    break;

                // Guest goes to cinema event.
                case HotelEventType.GOTO_CINEMA:
                    string guest = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);

                    // if the guest exists.
                    if (_hotel.Guests.Keys.Contains(guest))
                    {
                        Guest hotelGuest = _hotel.Guests[guest] as Guest;

                        // if the guest is not checking in or out
                        if (hotelGuest.StayState == StayState.Staying && !_hotel.Evacuating)
                            hotelGuest.FindAndTargetRoom(x => x is Cinema);
                    }

                    break;

                // Guest goes to fitness event.
                case HotelEventType.GOTO_FITNESS:
                    string fitnessGuest = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);
                    if (_hotel.Guests.Keys.Contains(fitnessGuest))
                    {
                        Guest hotelGuest = _hotel.Guests[fitnessGuest] as Guest;

                        if (hotelGuest.StayState == StayState.Staying && !_hotel.Evacuating)
                        {
                            hotelGuest.FindAndTargetRoom(x => x is Fitness);
                            hotelGuest.SetTimeToStayInRoom(float.Parse(evt.Data.Values.ElementAt(1)));
                        }
                    }
                    break;

                // Start the cinema event.
                case HotelEventType.START_CINEMA:
                    Cinema cinema = (Cinema)_hotel.Rooms.Where(x => x.ID == Int32.Parse(evt.Data.Values.ElementAt(0))).FirstOrDefault();
                    cinema?.StartMovie();

                    foreach (Person person in _hotel.Guests.Values)
                    {
                        if (person.CurrentRoom == cinema && person.Inside)
                        {
                            if (person is Guest)
                            {
                                (person as Guest).SetTimeToStayInRoom(ServiceLocator.Get<ConfigLoader>().GetConfig().FilmDuration);
                            }
                        }
                    }

                    break;

                // default. 
                default:
                    break;
            }
        }
    }
}

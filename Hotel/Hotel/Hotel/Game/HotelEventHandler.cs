using Hotel.Persons;
using Hotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hotel
{
    public class HotelEventHandler
    {
        private Hotel _hotel;

        public HotelEventHandler(Hotel hotel)
        {
            _hotel = hotel;
        }

        public void HandleEvent(HotelEvents.HotelEvent evt)
        {
            switch (evt.EventType)
            {
                // The 'NONE' event.
                case HotelEvents.HotelEventType.NONE:
                    break;

                // Check-in event.
                case HotelEvents.HotelEventType.CHECK_IN:
                    _hotel.AddGuest(evt.Data.Keys.ElementAt(0), Int32.Parse(Regex.Replace(evt.Data.Values.ElementAt(0), "[^0-9]+", string.Empty)));
                    break;

                // Check-out event.
                case HotelEvents.HotelEventType.CHECK_OUT:
                    // Get the guest that needs to be checked out.
                    string objectName = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);
                    if (_hotel.Guests.Keys.Contains(objectName))
                    {
                        (_hotel.Guests[objectName] as Guest).FindAndTargetRoom(x => x is Lobby && (x as Lobby).Receptionist != null);
                        (_hotel.Guests[objectName] as Guest).StayState = StayState.CheckOut;
                    }
                    break;

                // Cleaning Emergency event.
                case HotelEvents.HotelEventType.CLEANING_EMERGENCY:
                    // Get the specific room that has an emergency
                    Room roomWithEmergency = _hotel.Rooms.Where(x => x.ID == Int32.Parse(evt.Data.Values.ElementAt(0))).FirstOrDefault();
                    // Check if the room exists.
                    if (roomWithEmergency != null)
                        roomWithEmergency.SetEmergency(float.Parse(evt.Data.Values.ElementAt(1)));
                    break;

                // Evecuation event.
                case HotelEvents.HotelEventType.EVACUATE:
                    break;

                // Godzilla event.
                case HotelEvents.HotelEventType.GODZILLA: 
                    Console.WriteLine("AAAAAAAHHHHHHHH!");
                    // Call evacuate function?
                    break;

                // Guest needs food event.
                case HotelEvents.HotelEventType.NEED_FOOD:

                    string guestName = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);
                    if (_hotel.Guests.Keys.Contains(guestName))
                    {
                        Guest hotelGuest = _hotel.Guests[guestName] as Guest;

                        if(hotelGuest.StayState == StayState.Staying)
                            hotelGuest.FindAndTargetRoom(x => x is Cafe);
                    }
                        
                    break;

                // Guest goes to cinema event.
                case HotelEvents.HotelEventType.GOTO_CINEMA:
                    string guest = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);
                    if (_hotel.Guests.Keys.Contains(guest))
                    {
                        Guest hotelGuest = _hotel.Guests[guest] as Guest;

                        if (hotelGuest.StayState == StayState.Staying)
                            hotelGuest.FindAndTargetRoom(x => x is Cinema);
                    }

                    break;

                // Guest goes to fitness event.
                case HotelEvents.HotelEventType.GOTO_FITNESS:
                    string fitnessGuest = evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0);
                    if (_hotel.Guests.Keys.Contains(fitnessGuest))
                    {
                        Guest hotelGuest = _hotel.Guests[fitnessGuest] as Guest;

                        if (hotelGuest.StayState == StayState.Staying)
                        {
                            hotelGuest.FindAndTargetRoom(x => x is Fitness);
                            hotelGuest.LeaveRoomInTime(float.Parse(evt.Data.Values.ElementAt(1)));
                        }
                    }
                    break;

                // Start the cinema event.
                case HotelEvents.HotelEventType.START_CINEMA:
                    Cinema cinema = (Cinema)_hotel.Rooms.Where(x => x.ID == Int32.Parse(evt.Data.Values.ElementAt(0))).FirstOrDefault();
                    cinema?.StartMovie();

                    foreach(Person person in _hotel.Guests.Values)
                    {
                        if(person.CurrentRoom == cinema && person.Inside)
                        {
                            if(person is Guest)
                            {
                                (person as Guest).LeaveRoomInTime(ServiceLocator.Get<ConfigLoader>().GetConfig().FilmDuration);
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

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
                case HotelEvents.HotelEventType.NONE:
                    break;
                case HotelEvents.HotelEventType.CHECK_IN:
                    _hotel.AddGuest(evt.Data.Keys.ElementAt(0), Int32.Parse(Regex.Replace(evt.Data.Values.ElementAt(0), "[^0-9]+", string.Empty)));
                    break;
                case HotelEvents.HotelEventType.CHECK_OUT:
                    (_hotel.Persons[evt.Data.Keys.ElementAt(0) + evt.Data.Values.ElementAt(0)] as Guest).CheckOut(_hotel.Receptionist.CurrentRoom as Lobby);
                    break;
                case HotelEvents.HotelEventType.CLEANING_EMERGENCY:
                    Room roomWithEmergency = _hotel.Rooms.Where(x => x.ID == Int32.Parse(evt.Data.Values.ElementAt(0))).FirstOrDefault();
                    if (roomWithEmergency != null)
                    {
                        roomWithEmergency.State = RoomState.Emergency;
                        roomWithEmergency.EmergencyDuration = float.Parse(evt.Data.Values.ElementAt(1));
                    }
                    break;
                case HotelEvents.HotelEventType.EVACUATE:
                    break;
                case HotelEvents.HotelEventType.GODZILLA: 
                    Console.WriteLine("AAAAAAAHHHHHHHH!");
                    break;
                case HotelEvents.HotelEventType.NEED_FOOD:
                    break;
                case HotelEvents.HotelEventType.GOTO_CINEMA:
                    break;
                case HotelEvents.HotelEventType.GOTO_FITNESS:
                    break;
                case HotelEvents.HotelEventType.START_CINEMA:
                    Cinema cinema = (Cinema)_hotel.Rooms.Where(x => x.ID == Int32.Parse(evt.Data.Values.ElementAt(0))).FirstOrDefault();
                    cinema?.StartMovie();
                    break;
                default:
                    break;
            }
        }
    }
}

﻿using System;
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
                    break;
                case HotelEvents.HotelEventType.CLEANING_EMERGENCY:
                    break;
                case HotelEvents.HotelEventType.EVACUATE:
                    break;
                case HotelEvents.HotelEventType.GODZILLA:
                    break;
                case HotelEvents.HotelEventType.NEED_FOOD:
                    break;
                case HotelEvents.HotelEventType.GOTO_CINEMA:
                    break;
                case HotelEvents.HotelEventType.GOTO_FITNESS:
                    break;
                case HotelEvents.HotelEventType.START_CINEMA:
                    break;
                default:
                    break;
            }
        }
    }
}
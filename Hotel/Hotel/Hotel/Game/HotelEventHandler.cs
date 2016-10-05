using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            bool ehm = evt.EventType.Equals(HotelEvents.HotelEventType.CHECK_IN);
            bool wat = evt.EventType == HotelEvents.HotelEventType.CHECK_IN;

            bool eeh = Enum.Equals(evt.EventType, HotelEvents.HotelEventType.CHECK_IN);

            if(evt.EventType == HotelEvents.HotelEventType.CHECK_IN)
            {
                Console.WriteLine("TEST");
            }
            else
            {
                ;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HotelEvents;

namespace Hotel
{
    public class EventListener : HotelEventListener
    {
        private HotelEventHandler _hotelEventManager;

        public EventListener(HotelEventHandler hotelEventManager)
        {
            _hotelEventManager = hotelEventManager;
        }

        public void Notify(HotelEvent evt)
        {
            _hotelEventManager.HandleEvent(evt);
        }
    }
}

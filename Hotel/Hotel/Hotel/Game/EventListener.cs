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
        public void Notify(HotelEvent evt)
        {
            Console.WriteLine(evt.Message);
        }
    }
}

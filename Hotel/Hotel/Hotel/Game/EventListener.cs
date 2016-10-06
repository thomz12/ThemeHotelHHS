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

        //private static StreamWriter _writer = new StreamWriter("Events");

        public EventListener(HotelEventHandler hotelEventManager)
        {
            _hotelEventManager = hotelEventManager;
        }

        public void Exit()
        {
            //_writer.Close();
        }

        public void Notify(HotelEvent evt)
        {
            _hotelEventManager.HandleEvent(evt);
            /*try
            {
                _writer.WriteLine(evt.Time.ToString() + " HTE(s) in");
                _writer.WriteLine("Type: " + evt.EventType.ToString());

                foreach(KeyValuePair<string, string> kvp in evt.Data)
                {
                    _writer.WriteLine(kvp.Key + " : " + kvp.Value);
                }

                _writer.WriteLine();
            }
            catch
            {
                _writer.WriteLine("test...?");
            }*/
        }
    }
}

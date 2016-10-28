using System.Collections.Generic;

namespace Hotel.RoomsFactory
{
    public class RoomFactory
    {
        private Dictionary<string, IRoomFactoryComponent> _roomBuilders;

        public RoomFactory()
        {
            _roomBuilders = new Dictionary<string, IRoomFactoryComponent>();
        }

        public Room BuildRoom(Dictionary<string, string> data)
        {
            if(_roomBuilders.ContainsKey(data["AreaType"]))
                return _roomBuilders[data["AreaType"]].BuildRoom(data);

            return null;
        }

        /// <summary>
        /// Register a component to the factory.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="component"></param>
        public void RegisterComponent(string name, IRoomFactoryComponent component)
        {
            // Registers factory in dictionary.
            _roomBuilders.Add(name, component);
        }
    }
}

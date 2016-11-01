using System.Collections.Generic;

namespace Hotel.RoomsFactory
{
    public class RoomFactory
    {
        private Dictionary<string, IRoomFactoryComponent> _components;

        public RoomFactory()
        {
            _components = new Dictionary<string, IRoomFactoryComponent>();
        }

        public Room BuildRoom(Dictionary<string, string> data)
        {
            if(_components.ContainsKey(data["AreaType"]))
                return _components[data["AreaType"]].BuildRoom(data);

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
            _components.Add(name, component);
        }
    }
}

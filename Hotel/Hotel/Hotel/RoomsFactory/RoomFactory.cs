using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hotel.RoomsFactory
{
    public class RoomFactory
    {
        private Dictionary<string, IRoomFactoryComponent> _components;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RoomFactory()
        {
            _components = new Dictionary<string, IRoomFactoryComponent>();
        }

        /// <summary>
        /// Build a room.
        /// </summary>
        /// <param name="data">The data for this room.</param>
        /// <returns>A brand new room.</returns>
        public Room BuildRoom(Dictionary<string, string> data)
        {
            if(_components.ContainsKey(data["AreaType"]))
                return _components[data["AreaType"]].BuildRoom(data);

            return null;
        }

        /// <summary>
        /// Build a room.
        /// </summary>
        /// <param name="id">The room id.</param>
        /// <param name="name">The room name.</param>
        /// <param name="position">The room position.</param>
        /// <param name="size">The room size.</param>
        /// <returns></returns>
        public Room BuildRoom(int id, string name, Point position, Point size)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", id.ToString());
            data.Add("AreaType", name);
            data.Add("Position", position.X + "," + position.Y);
            data.Add("Dimension", size.X + "," + size.Y);

            return BuildRoom(data);
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

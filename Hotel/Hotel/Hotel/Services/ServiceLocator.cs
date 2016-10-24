using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Hotel
{
    /// Why are we using ServiceLocator pattern?
    /// - Because we need the objects that are stored in the container to be accessible everywhere.
    /// - The objects that are stored in the container are objects of which only one instance is needed.
    /// - The objects in the container all inherit from the IService interface.
    /// - The passing of the objects in the container is something which the client does not need to know about, so with this pattern we keep them out fo the constructor.

    /// <summary>
    /// A static object, following the ServiceLocator pattern.
    /// </summary>
    public static class ServiceLocator
    {
        private static GameServiceContainer _container = new GameServiceContainer();

        /// <summary>
        /// Get the object of a type out of the container.
        /// </summary>
        /// <typeparam name="T">The type of the object to request.</typeparam>
        /// <returns>The object that was requested or null.</returns>
        public static T Get<T>()
        {
            return (T)_container.GetService(typeof(T));
        }

        /// <summary>
        /// Adds an object of a type to the container.
        /// </summary>
        /// <typeparam name="T">The type of the object that needs to be added.</typeparam>
        /// <param name="service">The object to add.</param>
        public static void Add<T>(T service)
        {
            if(_container.GetService(typeof(T)) == null)
                _container.AddService(typeof(T), service);
        }

        /// <summary>
        /// Removes and object of a type from the container.
        /// </summary>
        /// <typeparam name="T">The type of the object to remove.</typeparam>
        public static void Remove<T>()
        {
            _container.RemoveService(typeof(T));
        }
    }
}

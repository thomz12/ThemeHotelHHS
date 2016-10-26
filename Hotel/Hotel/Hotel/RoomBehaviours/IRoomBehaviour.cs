using Hotel.Persons;

namespace Hotel
{
    /// <summary>
    /// Used for implementing behaviour for the guest.
    /// </summary>
    public interface IRoomBehaviour
    {
        /// <summary>
        /// Call this when a person or room needs to do something when the person arrives at the room.
        /// </summary>
        /// <param name="room">The room in question.</param>
        /// <param name="person">The person in question.</param>
        void OnArrival(Room room, Person person);

        /// <summary>
        /// Call this when a person or room needs to do something when the person departs from the room.
        /// </summary>
        /// <param name="room">The room in question.</param>
        /// <param name="person">The person in question.</param>
        void OnDeparture(Room room, Person person);

        /// <summary>
        /// Call this when a person or room needs to do something when the person passes in front of the room.
        /// </summary>
        /// <param name="room">The room in question.</param>
        /// <param name="person">The person in question.</param>
        void OnPassRoom(Room room, Person person);
    }
}

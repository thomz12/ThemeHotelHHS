using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Persons;

namespace Hotel.Rooms
{
    public class Lobby : Room
    {
        /// <summary>
        /// The receptionist.
        /// </summary>
        public Receptionist Receptionist { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The room ID.</param>
        /// <param name="position">The room position.</param>
        /// <param name="size">The room size.</param>
        public Lobby(int id, Point position, Point size) : base(id, position, size)
        {
            // Load assets.
            Sprite.LoadSprite("1x1Lobby");
            // Set name
            Name = "Lobby";

            // Make a new behaviour.
            RoomBehaviour = new LobbyBehaviour();
        }

        /// <summary>
        /// Adds a guest to the queue of the receptionist.
        /// </summary>
        /// <param name="guest">The guest to check in.</param>
        public void CheckIn(Guest guest)
        {
            // Nullcheck
            if (Receptionist == null)
                return;

            // Add guest to the queue.
            Receptionist.CheckInQueue.Enqueue(guest);
            // Set the guest's task to queueing.
            guest.CurrentTask = PersonTask.InQueue;
        }

        /// <summary>
        /// Adds the guest to the queue of the receptionist.
        /// </summary>
        /// <param name="guest">the guest to check out.</param>
        public void CheckOut(Guest guest)
        {
            // Nullcheck
            if (Receptionist == null)
                return;

            // Add guest tot he queue.
            Receptionist.CheckOutQueue.Enqueue(guest);
            // Set the guest's task to queueing.
            guest.CurrentTask = PersonTask.InQueue;
        }

        /// <summary>
        /// Removes the guest from checkin and checkout queues.
        /// </summary>
        /// <param name="guest">The guest that needs to be removed.</param>
        public void RemoveFromQueues(Guest guest)
        {
            // Remove this guest from the queues in front of the lobbyist because the guest died.
            if (Receptionist.CheckInQueue.Contains(guest))
            {
                List<Guest> inQueue = Receptionist.CheckInQueue.ToList();
                inQueue.Remove(guest);
                Receptionist.CheckInQueue = new Queue<Guest>(inQueue);
            }
            if (Receptionist.CheckOutQueue.Contains(guest))
            {
                List<Guest> outQueue = Receptionist.CheckOutQueue.ToList();
                outQueue.Remove(guest);
                Receptionist.CheckOutQueue = new Queue<Guest>(outQueue);
            }
        }
    }
}

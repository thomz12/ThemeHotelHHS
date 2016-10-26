using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Persons;
using Hotel.RoomBehaviours;

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
            Sprite.LoadSprite("1x1Lobby");
            Name = "Lobby";

            RoomBehaviour = new LobbyBehaviour();
        }

        /// <summary>
        /// Adds a guest to the queue of the receptionist.
        /// </summary>
        /// <param name="guest">The guest to check in.</param>
        public void CheckIn(Guest guest)
        {
            if (Receptionist == null)
                return;

            Receptionist.CheckinQueue.Add(guest);
            guest.CurrentTask = PersonTask.InQueue;
        }

        /// <summary>
        /// Adds the guest to the queue of the receptionist.
        /// </summary>
        /// <param name="guest">the guest to check out.</param>
        public void CheckOut(Guest guest)
        {
            if (Receptionist == null)
                return;

            Receptionist.CheckOutQueue.Add(guest);
            guest.CurrentTask = PersonTask.InQueue;
        }

        /// <summary>
        /// Removes the guest from checkin and checkout queues.
        /// </summary>
        /// <param name="guest">The guest that needs to be removed.</param>
        public void RemoveFromQueues(Guest guest)
        {
            if(Receptionist.CheckinQueue.Contains(guest))
                Receptionist.CheckinQueue.Remove(guest);
            if (Receptionist.CheckOutQueue.Contains(guest))
                Receptionist.CheckOutQueue.Remove(guest);
        }
    }
}

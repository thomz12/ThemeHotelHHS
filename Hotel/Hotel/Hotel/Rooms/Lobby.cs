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
        public Receptionist Receptionist { get; set; }

        public Lobby(int id, Point position, Point size) : base(id, position, size)
        {
            Sprite.LoadSprite("1x1Lobby");
            Name = "Lobby";

            roomBehaviour = new LobbyBehaviour();
        }

        public void CheckIn(Guest guest)
        {
            Receptionist.CheckinQueue.Add(guest);
            guest.CurrentTask = PersonTask.InQueue;
        }

        public void CheckOut(Guest guest)
        {
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

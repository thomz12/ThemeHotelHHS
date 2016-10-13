using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework;

namespace Hotel.Persons
{
    public class Receptionist : Person
    {
        public List<Guest> CheckinQueue { get; set; }
        public List<Guest> CheckOutQueue { get; set; }

        private List<Room> _rooms;
        private float _workSpeed;
        private float _checkInTimer;
        private float _checkOutTimer;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="room">The room to spawn the guest in.</param>
        /// <param name="rooms">All the rooms in the hotel.</param>
        /// <param name="survivability">The time the guest can stand in a queue without dieing.</param>
        /// <param name="walkingSpeed">The speed at which the guest walks.</param>
        /// <param name="workDuration">The speed at which the receptionist checks in/out.</param>
        public Receptionist(Room room, List<Room> rooms) : base(room)
        {
            Name = "Receptionist";
            Sprite.LoadSprite("Receptionist");
            Sprite.SetSize(new Point(Sprite.Texture.Width, Sprite.Texture.Height));
            CurrentRoom = room;

            CheckinQueue = new List<Guest>();
            CheckOutQueue = new List<Guest>();

            // Load settings from the config.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();
            _workSpeed = config.ReceptionistWorkLenght;

            _checkInTimer = _workSpeed;
            _checkOutTimer = _workSpeed;

            _rooms = rooms;

            if (room is Rooms.Lobby)
                (room as Rooms.Lobby).Receptionist = this;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (CheckinQueue.Count > 0)
                CheckinQueue[0].CurrentTask = PersonTask.Waiting;

            if (CheckOutQueue.Count > 0)
                CheckOutQueue[0].CurrentTask = PersonTask.Waiting;

            if(CheckinQueue.Count > 0)
                _checkInTimer -= deltaTime;

            if(_checkInTimer <= 0 && CheckinQueue.Count > 0)
            {
                _checkInTimer = _workSpeed;

                Guest CheckThisGuyIn = CheckinQueue.First();
                CheckIn(CheckThisGuyIn);

                CheckinQueue.RemoveAt(0);
            }

            if(CheckOutQueue.Count > 0)
                _checkOutTimer -= deltaTime;

            if(_checkOutTimer <= 0 && CheckOutQueue.Count > 0)
            {
                _checkOutTimer = _workSpeed;
                CheckOut(CheckOutQueue.First());
                CheckOutQueue.RemoveAt(0);
            }

        }

        /// <summary>
        /// Assigns a room to a guest, and checks him in. 
        /// </summary>
        /// <param name="guest"></param>
        private void CheckIn(Guest guest)
        {
            List<GuestRoom> potentialRooms = _rooms.OfType<GuestRoom>().Where(x => x.Classification == guest.Classification && x.Guest == null).ToList();

            // Check first if there is a room available.
            if (potentialRooms.Count == 0)
            {
                // There is no room available so send the person back outside.
                guest.FindAndTargetRoom(x => x.Name.Equals("Outside"));
            }
            else
            {
                // TODO: Let guest 'upgrade' in rooms if a room of their classification is not available.
                // Check this person in.
                guest.Room = potentialRooms.First();
                guest.StayState = StayState.Staying;
                potentialRooms.First().Guest = guest;
                potentialRooms.First().State = RoomState.Occupied;
                guest.FindAndTargetRoom(x => x == guest.Room);
            }
        }

        /// <summary>
        /// Checks out the guest
        /// </summary>
        /// <param name="guest">The guest to check out.</param>
        private void CheckOut(Guest guest)
        {
            if (guest.Room != null)
            {
                if(guest.Room.State != RoomState.Emergency && guest.Room.State != RoomState.InCleaning)
                    guest.Room.State = RoomState.Dirty;

                guest.Room.Guest = null;
                guest.Room = null;
                guest.FindAndTargetRoom(x => x.Name.Equals("Outside"));
            }
        }
    }
}

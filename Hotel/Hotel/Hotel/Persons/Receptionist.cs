using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel;
using Microsoft.Xna.Framework.Content;
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
        /// <param name="content">The content manager used to load in images.</param>
        public Receptionist(ContentManager content, Room room, List<Room> rooms, float survivability, float walkingSpeed, float workDuration) : base(content, room, -1, walkingSpeed)
        {
            Name = "Receptionist";
            Sprite.LoadSprite("Receptionist");
            Sprite.SetSize(new Point(Sprite.Texture.Width, Sprite.Texture.Height));
            CurrentRoom = room;

            CheckinQueue = new List<Guest>();
            CheckOutQueue = new List<Guest>();

            _workSpeed = workDuration;

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

        private void CheckIn(Guest guest)
        {
            List<GuestRoom> potentialRooms = _rooms.OfType<GuestRoom>().Where(x => x.Classification == guest.Classification && x.Guest == null).ToList();

            // Check first if there is a room available.
            if (potentialRooms.Count == 0)
            {
                // There is no room available so send the person back outside.
                guest.TargetRoom = _rooms[0];
            }
            else
            {
                // Check this person in.
                guest.Room = potentialRooms.First();
                potentialRooms.First().Guest = guest;
                potentialRooms.First().State = RoomState.Occupied;
                guest.TargetRoom = guest.Room;
            }
        }

        private void CheckOut(Guest guest)
        {
            if (guest.Room != null)
            {
                guest.Room.State = RoomState.Dirty;
                guest.Room.Guest = null;
                guest.Room = null;
                guest.TargetRoom = _rooms[0];
            }
        }
    }
}

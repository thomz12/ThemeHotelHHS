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
        public Queue<Guest> CheckinQueue { get; set; }
        public Queue<Guest> CheckOutQueue { get; set; }

        private List<Room> _rooms;
        private float _workSpeed;
        private float _checkInTimer;
        private float _checkOutTimer;
        private float _highestClassificationRoom;
        private int _distanceBetweenThePeopleInTheQueues;

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

            CheckinQueue = new Queue<Guest>();
            CheckOutQueue = new Queue<Guest>();

            _highestClassificationRoom = 6;
            _distanceBetweenThePeopleInTheQueues = 15;

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
            // Update the base.
            base.Update(deltaTime);

            // If people are being helped, it is rude for them to die! So change their task to Waiting, which stops their death timers. 
            if (CheckinQueue.Count > 0)
                CheckinQueue.First().CurrentTask = PersonTask.Waiting;
            if (CheckOutQueue.Count > 0)
                CheckOutQueue.First().CurrentTask = PersonTask.Waiting;

            // Reduce the time of the timer for the checkin by the time that has passed.
            if(CheckinQueue.Count > 0)
                _checkInTimer -= deltaTime;

            // The previous checkin has succeeded.
            if(_checkInTimer <= 0 && CheckinQueue.Count > 0)
            {
                // Reset the timer for the next guest.
                _checkInTimer = _workSpeed;

                // Check this guy in and kick him out of the queue.
                CheckIn(CheckinQueue.Dequeue());
            }

            // Reduce the time of the timer for the checkout by the time that has passed.
            if(CheckOutQueue.Count > 0)
                _checkOutTimer -= deltaTime;
            
            // The previous checkout has succeeded.
            if(_checkOutTimer <= 0 && CheckOutQueue.Count > 0)
            {
                // Reset the timer for the next guest.
                _checkOutTimer = _workSpeed;

                // Check this guy out and kick him out of the queue.
                CheckOut(CheckOutQueue.Dequeue());
            }
            
            // For visual reasons we change the world position of the guests according to the position in the queue, so they form a nice row.
            for(int i = 0; i < CheckinQueue.Count; i++)
            {
                CheckinQueue.ElementAt(i).Position = new Vector2((Position.X - i * _distanceBetweenThePeopleInTheQueues), CheckinQueue.ElementAt(i).Position.Y);
            }
            for (int i = 0; i < CheckOutQueue.Count; i++)
            {
                CheckOutQueue.ElementAt(i).Position = new Vector2((Position.X + Sprite.DrawDestination.Width + (i * _distanceBetweenThePeopleInTheQueues)), CheckOutQueue.ElementAt(i).Position.Y);
            }
        }

        /// <summary>
        /// Assigns a room to a guest, and checks him in. 
        /// </summary>
        /// <param name="guest">The guest to checkin.</param>
        private void CheckIn(Guest guest)
        {
            // Put the function on a loop, because the guest can get an upgrade when there are no rooms available for his desired classification.
            while (guest.Classification < _highestClassificationRoom)
            {
                // Put all the empty rooms of the desired classification in a list.
                List<GuestRoom> potentialRooms = _rooms.OfType<GuestRoom>().Where(x => x.Classification == guest.Classification && x.Guest == null).ToList();

                // Check first if there is a room available.
                if (potentialRooms.Count == 0)
                {
                    // There is no room availiable.
                    // If the classification gets too high (higher than the maximum classification) we must stop this person from entering our hotel!
                    if (guest.Classification >= _highestClassificationRoom - 1)
                    {
                        // Check him out.
                        CheckOut(guest);
                    }

                    // Increase the clasification of the guest by one.
                    guest.Classification++;
                }
                else
                {
                    // There is a room available.
                    // Give him a room.
                    guest.Room = potentialRooms.First();
                    // Set his staystate.
                    guest.StayState = StayState.Staying;
                    // The room now also has a guest.
                    potentialRooms.First().Guest = guest;
                    // Occupy this room.
                    potentialRooms.First().State = RoomState.Occupied;
                    // Set the guest to target the newly recieved room.
                    guest.FindAndTargetRoom(x => x == guest.Room);
                    // Rip.
                    break;
                }
            }
        }

        /// <summary>
        /// Checks out the guest.
        /// </summary>
        /// <param name="guest">The guest to check out.</param>
        private void CheckOut(Guest guest)
        {
            // Nullcheck.
            if (guest.Room != null)
            {
                // Set the guests room to dirty, because the guest made a mess of it.
                if(guest.Room.State != RoomState.Emergency && guest.Room.State != RoomState.InCleaning)
                    guest.Room.State = RoomState.Dirty;

                // Remove the guest for the room.
                guest.Room.Guest = null;
                // Remove the room from the guest? (i guess)
                guest.Room = null;
            }

            // Set the guest to target outside.
            guest.FindAndTargetRoom(x => x.Name.Equals("Outside"));
        }
    }
}

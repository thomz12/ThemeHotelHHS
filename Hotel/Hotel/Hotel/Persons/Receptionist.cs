﻿using System;
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
        public Receptionist(ContentManager content, Room room, List<Room> rooms, float walkingSpeed) : base(content, room, walkingSpeed)
        {
            Name = "Receptionist";
            Sprite.LoadSprite("Receptionist");
            Sprite.SetSize(new Point(Sprite.Texture.Width, Sprite.Texture.Height));
            CurrentRoom = room;

            CheckinQueue = new List<Guest>();
            CheckOutQueue = new List<Guest>();

            // TODO: make this adjustable.
            _workSpeed = 1.0f;

            _rooms = rooms;

            if (room is Rooms.Lobby)
                (room as Rooms.Lobby).Receptionist = this;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _checkInTimer -= deltaTime;

            if(_checkInTimer <= 0 && CheckinQueue.Count > 0)
            {
                _checkInTimer = _workSpeed;
                CheckIn(CheckinQueue.First());
                CheckinQueue.RemoveAt(0);
            }

            _checkOutTimer -= deltaTime;
        }

        private void CheckIn(Guest guest)
        {
            List<GuestRoom> potentialRooms = _rooms.OfType<GuestRoom>().Where(x => x.Classification == guest.Classification && x.Guest == null).ToList();

            if (potentialRooms.Count == 0)
            {
                guest.TargetRoom = _rooms[0];
            }
            else
            {
                guest.Room = potentialRooms.First();
                potentialRooms.First().Guest = guest;
                guest.TargetRoom = guest.Room;
            }
        }

        private void CheckOut(Guest guest)
        {

        }
    }
}

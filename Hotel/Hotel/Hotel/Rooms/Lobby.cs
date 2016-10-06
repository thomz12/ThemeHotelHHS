using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public Lobby(ContentManager content, int id, Point position, Point size) : base(content, id, position, size)
        {
            Sprite.LoadSprite("1x1Lobby");
            Name = "Lobby";
        }

        public void CheckIn(Guest guest)
        {
            Receptionist.CheckinQueue.Add(guest);
        }

        public void CheckOut(Guest guest)
        {
            Receptionist.CheckOutQueue.Add(guest);
        }
    }
}

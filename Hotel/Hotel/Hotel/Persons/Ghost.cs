﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Persons
{
    public class Ghost : Person
    {
        public Ghost(Room room) : base(room)
        {
            Sprite.LoadSprite("Ghost");
            FindAndTargetRoom(x => x.Name.Equals("Outside"));

            Arrival += Arrival_AtRoom;
            Death += Ghost_Remove;
        }

        private void Ghost_Remove(object sender, EventArgs e)
        {
            // Do Something when the ghost dies (gets removed)
        }

        private void Arrival_AtRoom(object sender, EventArgs e)
        {
            // KILL YOURSELF
            if(CurrentRoom.Name.Equals("Outside"))
            {
                OnDeath(new EventArgs());
            }
        }
    }
}
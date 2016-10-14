﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Persons
{
    public class CleanerGhost : Person
    {
        public CleanerGhost(Room room) : base(room)
        {
            Sprite.LoadSprite("Ghost");

            _survivabilityTime = -1;

            _pathFinder.UseElevator = false;
            FindAndTargetRoom(x => x.Name.Equals("Outside"));
        }

        public override void OnArrival()
        {
            // KILL YOURSELF
            if(CurrentRoom.Name.Equals("Outside"))
            {
                Remove(new EventArgs());
            }
        }
    }
}

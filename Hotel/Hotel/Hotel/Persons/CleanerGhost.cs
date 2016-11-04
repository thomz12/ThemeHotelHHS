using Hotel.Rooms;
using System;
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

            Evacuating = true;
            Ghost = true;

            Name = "Cleaner Ghost";

            _pathFinder.UseElevator = false;
            FindAndTargetRoom(x => (x is EmptyRoom) && (x as EmptyRoom).Entrance);
        }

        public override void OnArrival()
        {
            // KILL YOURSELF (without dying)
            if(CurrentRoom.Name.Equals("Outside"))
            {
                Remove();
            }
        }
    }
}

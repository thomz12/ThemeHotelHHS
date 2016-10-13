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
            _pathFinder.UseElevator = false;

            Sprite.LoadSprite("Ghost");
            FindAndTargetRoom(x => x.Name.Equals("Outside"));

            Arrival += Arrival_AtRoom;
            Death += Ghost_Remove;
        }

        private void Ghost_Remove(object sender, EventArgs e)
        {
            // Do Something when the ghost dies (gets removed)
            RemoveMe(new EventArgs());
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

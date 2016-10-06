using Hotel.Persons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class GuestRoom : Room
    {
        public int Classification { get; private set; }

        public bool Dirty { get; set; }
        public Guest Guest { get; set; } 

        public GuestRoom(ContentManager content, int id, Point position, Point size, int classification) : base(content, id, position, size)
        {
            // Check for the room size and load in specified sprites.
            if(size.X == 2 && size.Y == 1)
            {
                Sprite.LoadSprite("2x1GuestRoom");
            }
            else if(size.X == 2 && size.Y == 2)
            {
                Sprite.LoadSprite("2x2GuestRoom");
            }
            else
            {
                // Load default sprite
                Sprite.LoadSprite("1x1GuestRoom");
            }
            Name = $"{classification} Star Guest Room";
            Classification = classification;

            // All the rooms are default Vacant
            State = RoomState.Vacant;
        }

        public override string ToString()
        {
            return base.ToString() + $"{Environment.NewLine}Guest: {(Guest == null ? "none" : Guest.Name)}";
        }
    }
}

using Hotel.Persons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class GuestRoom : Room
    {
        public int Classification { get; private set; }

        public Guest Guest { get; set; } 

        public GuestRoom(int id, Point position, Point size, int classification) : base(id, position, size)
        {
            // Check for the room size and load in specified sprites.
            if(size.X == 2 && size.Y == 1)
                Sprite.LoadSprite("2x1GuestRoom");
            else if(size.X == 2 && size.Y == 2)
                Sprite.LoadSprite("2x2GuestRoom");
            else
                // Load default sprite
                Sprite.LoadSprite("1x1GuestRoom");

            Name = $"{classification} Star Guest Room";
            Classification = classification;

            // All the rooms are default Vacant
            State = RoomState.Vacant;
        }

        public override void Update(float deltaTime)
        {
            UpdateSprite();
            base.Update(deltaTime);
        }

        private void UpdateSprite()
        {
            // Because we use the same naming for all the sprites we can work with filenames instead of tons of ifs and switches.
            // Build a filename which changes based on the properties of the room.
            string FileName = "";

            if (RoomSize.X == 2 && RoomSize.Y == 1)
                FileName += "2x1";
            else if (RoomSize.X == 2 && RoomSize.Y == 2)
                FileName += "2x2";
            else
                // Load default sprite
                FileName += "1x1";

            FileName += "GuestRoom";

            switch (State)
            {
                case RoomState.None:
                    break;
                case RoomState.Vacant:
                    break;
                case RoomState.Dirty:
                    FileName += "Dirty";
                    break;
                case RoomState.InCleaning:
                    FileName += "Cleaning";
                    break;
                case RoomState.Occupied:
                    FileName += "Occupied";
                    break;
                default:
                    break;
            }

            Sprite.LoadSprite(FileName);
        }

        public override string ToString()
        {
            return base.ToString() + $"{Environment.NewLine}Guest: {(Guest == null ? "none" : Guest.Name)}";
        }
    }
}

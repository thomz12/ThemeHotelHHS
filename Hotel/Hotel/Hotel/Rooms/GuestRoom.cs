using Hotel.Persons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.RoomBehaviours;

namespace Hotel.Rooms
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

            // Create a new behaviour.
            RoomBehaviour = new GuestRoomBehaviour();

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
            string fileName = "";

            fileName += RoomSize.X + "x" + RoomSize.Y;

            fileName += "GuestRoom";

            switch (State)
            {
                case RoomState.None:
                    break;
                case RoomState.Vacant:
                    break;
                case RoomState.Dirty:
                    fileName += "Dirty";
                    break;
                case RoomState.InCleaning:
                    fileName += "Cleaning";
                    break;
                case RoomState.Occupied:
                    fileName += "Occupied";
                    break;
                default:
                    break;
            }

            // Try to load the sprite from the string we just build.
            Sprite.LoadSprite(fileName);

            // If the sprite was not found, default to the 1x1GuestRoom sprite.
            if (Sprite.Texture == null)
                Sprite.LoadSprite("1x1GuestRoom");
        }

        public override string ToString()
        {
            return base.ToString() + $"{Environment.NewLine}Guest: {(Guest == null ? "none" : Guest.Name)}";
        }
    }
}

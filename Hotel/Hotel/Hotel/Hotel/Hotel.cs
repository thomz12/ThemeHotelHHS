using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class Hotel
    {
        public List<Room> Rooms { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content manager used to load in room images.</param>
        public Hotel(ContentManager content)
        {
            Rooms = new List<Room>();
            
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (i == 1 ||  i == 5 || i == 9)
                    {
                        Rooms.Add(new ElevatorShaft(content));
                    }
                    else
                    {
                        Rooms.Add(new GuestRoom(content));
                    }
                    Rooms.Last().Position = new Vector2(i * 60, j * 30);
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            foreach(Room room in Rooms)
            {
                room.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            foreach(Room room in Rooms)
            {
                room.Draw(batch, gameTime);
            }
        }
    }
}

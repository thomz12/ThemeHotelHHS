using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.Rooms;

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
            
            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Rooms.Add(new Lobby(content));
                    }
                    else if (i > 1)
                    {
                        Rooms.Add(new ElevatorShaft(content, j));
                    }
                    else
                    {
                        Rooms.Add(new GuestRoom(content));
                    }
                    Rooms.Last().Position = new Vector2(i * Rooms.Last().Sprite.Texture.Width, j * Rooms.Last().Sprite.Texture.Height);
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

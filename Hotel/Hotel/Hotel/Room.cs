using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class Room : IDrawable
    {
        // SPRITE
        public int Width { get; set; }
        public Vector2 Position { get; set; }
        public int Weight { get; set; }

        public bool Visible { get; set; }

        public int DrawOrder { get; set; }

        public Room()
        {

        }

        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

        public void Draw(GameTime gameTime)
        {
            
        }
    }
}

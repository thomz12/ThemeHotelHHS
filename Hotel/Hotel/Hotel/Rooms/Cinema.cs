using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Rooms
{
    public class Cinema : Room
    {
        public int Duration { get; private set; }
        public bool Open { get; private set; }
        private float _timeLeft;

        public event EventHandler Finished;

        public Cinema(ContentManager content, int id, Point position, Point size, int duration) : base(content, id, position, size)
        {
            Sprite.LoadSprite("2x2Cinema");
            Name = "Cinema";
            Duration = duration;

            Finished += Cinema_Finished;
        }

        private void Cinema_Finished(object sender, EventArgs e)
        {
            Open = true;
        }

        private void OnFinish(EventArgs args)
        {
            if (Finished != null)
                Finished(this, args);
        }

        /// <summary>
        /// Starts the movie
        /// </summary>
        public void StartMovie()
        {
            Sprite.LoadSprite("2x2CinemaPlaying");
            Open = false;
            _timeLeft = Duration;
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="deltaTime">the time elapsed</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_timeLeft >= 0)
            {
                _timeLeft -= deltaTime;
            }
            else if(Open == false)
            {
                Sprite.LoadSprite("2x2Cinema");
                OnFinish(new EventArgs());
            }
        }
    }
}

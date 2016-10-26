using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.RoomBehaviours;

namespace Hotel.Rooms
{
    public class Cinema : Room
    {
        public int Duration { get; private set; }
        public bool Open { get; private set; }
        private float _timeLeft;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Room ID</param>
        /// <param name="position">Room position</param>
        /// <param name="size">Room size</param>
        public Cinema(int id, Point position, Point size) : base(id, position, size)
        {
            Sprite.LoadSprite("2x2Cinema");
            Name = "Cinema";

            // Load settings from the config.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();
            Duration = config.FilmDuration;

            RoomBehaviour = new CinemaBehaviour();
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
            else
            {
                if (Open == false)
                {
                    Sprite.LoadSprite("2x2Cinema");
                    Open = true;
                }
            }
        }
    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    /// <summary>
    /// Makes a beautifull background for the simulation
    /// </summary>
    public class Background
    {
        private Sprite _sprite;
        private Camera _cam;

        public Background(Camera cam)
        {
            _cam = cam;
            _sprite = new Sprite();
            _sprite.LoadSprite("Background");
        }

        public void Update(float deltaTime)
        {
            // Try to counter the offset of the camera in the scene.
            _sprite.SetPosition(new Point((int)(-_cam.TransformMatrix.M41 / _cam.Zoom), (int)(_cam.TransformMatrix.M42 / _cam.TransformMatrix.M22)));
            _sprite.SetSize(new Point((int)(_cam.Size.X / _cam.Zoom), (int)(_cam.Size.Y / _cam.Zoom)));
            _sprite.Update(deltaTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            _sprite.Draw(batch, gameTime);
        }
    }
}

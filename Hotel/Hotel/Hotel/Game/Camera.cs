using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    public class Camera
    {
        // Constants
        public const float MAXZOOM = 1.0f;
        public const float MINZOOM = 0.1f;

        public Matrix TransformMatrix { get; set; }
        public float Rotation { get; set; }
        public Vector2 CamPosition { get; set; }
        public float Zoom { get; set; } 

        private MouseState _mouseState;

        /// <summary>
        /// Constructor
        /// </summary>
        public Camera()
        {
            Zoom = 0.6f;
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="device">The graphics device.</param>
        /// <param name="deltaTime">The gametime.</param>
        public void Update(GraphicsDevice device, float deltaTime)
        {
            // The current mouse state
            MouseState curState = Mouse.GetState();

            // Move the camera position if we have the mouse button pressed.
            if (Input.Instance.IsRightMouseButtonPressed())
                CamPosition = new Vector2(CamPosition.X - ((_mouseState.X - curState.X) / Zoom), CamPosition.Y - ((_mouseState.Y - curState.Y) / Zoom));

            if (curState.ScrollWheelValue > _mouseState.ScrollWheelValue)
                Zoom += 0.1f;

            if (curState.ScrollWheelValue < _mouseState.ScrollWheelValue)
                Zoom -= 0.1f;

            if (Zoom < MINZOOM)
                Zoom = MINZOOM;

            if (Zoom > MAXZOOM)
                Zoom = MAXZOOM;

            // Transpose the transform matrix.
            TransformMatrix = Matrix.CreateTranslation(new Vector3(CamPosition.X, CamPosition.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(device.Viewport.Width * 0.5f, device.Viewport.Height * 0.5f, 0));

            // Set the old mousestate to this one. (to compare it for the next frame)
            _mouseState = curState;
        }
    }
}

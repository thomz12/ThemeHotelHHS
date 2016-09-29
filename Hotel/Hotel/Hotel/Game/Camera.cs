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
        public const float MINZOOM = 0.5f;

        public Matrix TransformMatrix { get; set; }
        public float Rotation { get; set; }
        public Vector2 CamPosition { get; set; }
        public float Zoom { get; set; }
        public bool Controlable { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Camera()
        {
            Zoom = 0.6f;
            Controlable = false;
        }


        /// <summary>
        /// Gets the World coordinates from the mouse's position
        /// </summary>
        /// <param name="cam">The main camera.</param>
        /// <returns></returns>
        public Point ScreenToWorld(Point screenSpace)
        {
            return new Point((int)(((float)screenSpace.X - (int)(TransformMatrix.M41)) / Zoom), (int)(((float)screenSpace.Y - (int)(TransformMatrix.M42)) / Zoom));
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        /// <param name="device">The graphics device.</param>
        /// <param name="deltaTime">The time between frames.</param>
        public void Update(int width, int height, float deltaTime)
        {
            if (Controlable)
            {
                // Move the camera position if we have the mouse button pressed.
                if (Input.Instance.IsRightMouseButtonPressed())
                    CamPosition = new Vector2(CamPosition.X - (Input.Instance.GetMouseDelta().X / Zoom), CamPosition.Y - (Input.Instance.GetMouseDelta().Y / Zoom));

                // Zoom in or out.
                Zoom += (float)(Input.Instance.GetScrollDelta() / 10000.0f);

                if (Zoom < MINZOOM)
                    Zoom = MINZOOM;

                if (Zoom > MAXZOOM)
                    Zoom = MAXZOOM;

                // Transpose the transform matrix.
            }

            TransformMatrix = Matrix.CreateTranslation(new Vector3(CamPosition.X, CamPosition.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(width * 0.5f, height * 0.5f, 0));
        }
    }
}

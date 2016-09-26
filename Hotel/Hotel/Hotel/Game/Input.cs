using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel
{
    /// <summary>
    /// Using the Singleton Pattern to have access to the on key press events everywhere.
    /// </summary>
    class Input
    {
        // The instance of this object
        private static Input _instance;
        
        // Make the instance when this instance is null
        public static Input Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new Input();
                return _instance;
            }
        }

        // Keyboard states
        private KeyboardState _priorKeyboardState;
        private KeyboardState _curKeyboardState;

        /// <summary>
        /// Constructor
        /// </summary>
        private Input()
        {
            _curKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Updates every frame.
        /// </summary>
        /// <param name="gameTime">The gametime.</param>
        public void Update(GameTime gameTime)
        {
            _priorKeyboardState = _curKeyboardState;
            _curKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Returns true if the key is down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>Boolean, true if pressed, false if released.</returns>
        public bool IsKeyPressed(Keys key)
        {
            if (_curKeyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if the key is up.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>Boolean, true if released, false if pressed.</returns>
        public bool IsKeyReleased(Keys key)
        {
            if (_curKeyboardState.IsKeyUp(key))
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if the key is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>Boolean, true if pressed, else false.</returns>
        public bool OnKeyPress(Keys key)
        {
            if (_curKeyboardState.IsKeyDown(key) && _priorKeyboardState.IsKeyUp(key))
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if the key is released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>Boolean, true if released, else false.</returns>
        public bool OnKeyRelease(Keys key)
        {
            if (_curKeyboardState.IsKeyUp(key) && _priorKeyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        /// <summary>
        /// Gets the World coordinates from the mouse's position
        /// </summary>
        /// <param name="mousePos">The current position of the mouse.</param>
        /// <param name="cam">The main camera.</param>
        /// <returns></returns>
        public Point ScreenToWorld(Point mousePos, Camera cam)
        {
            return new Point(mousePos.X - (int)cam.TransformMatrix.M41, mousePos.Y - (int)cam.TransformMatrix.M42);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Nuclex.UserInterface;

namespace Hotel
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Hotel _hotel;
        private Camera _camera;
        private GameObject _selected;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Blue Hotel";
            IsMouseVisible = true;

            // Enable v-sync
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;

            //_graphics.IsFullScreen = true;

            // Disable the fixed time step, causes low frame rates on some computers.
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _hotel = new Hotel(Content);
            _camera = new Camera();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Input.Instance.IsKeyPressed(Keys.Escape))
                this.Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * 1.0f;

            if(_selected != null)
                _selected.Sprite.Color = Color.White;

            _selected = _hotel.GetObject(_camera.ScreenToWorld(Input.Instance.GetMousePos()));

            if (_selected != null)
            {
                _selected.Sprite.Color = Color.LightBlue;

                if (Input.Instance.OnLeftMouseButtonRelease())
                {
                    Random r = new Random();
                    if (_selected is ElevatorShaft)
                    {
                        ElevatorShaft es = (ElevatorShaft)_selected;

                        int target = r.Next(0, 6);

                        while (es.RoomPosition.Y == target)
                        {
                            target = r.Next(0, 6);
                        }

                        es.CallElevator(target);
                    }
                }
            }

            _hotel.Update(deltaTime);
            _camera.Update(GraphicsDevice, deltaTime);
            Input.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _camera.TransformMatrix);

            _hotel.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

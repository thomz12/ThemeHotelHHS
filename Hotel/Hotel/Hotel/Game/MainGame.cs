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
using HotelEvents;

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
        private HotelObject _mouseIsOver;
        private HotelObject _wasSelected;
        private DetailedInformation _DI;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Hotel";
            IsMouseVisible = true;

            // Enable v-sync
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;

            _graphics.IsFullScreen = false;

            Exiting += MainGame_Exiting;

            HotelEventManager.Start();

            HotelEventListener listener = new EventListener();
            HotelEventManager.Register(listener);

            // Disable the fixed time step, causes low frame rates on some computers.
            IsFixedTimeStep = false;
        }

        private void MainGame_Exiting(object sender, EventArgs e)
        {
            HotelEventManager.Stop();
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

            _DI = new DetailedInformation(Content);

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
            {
                Exit();
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * 1.0f;

            // Check for mouseover
            MouseOver();

            _hotel.Update(deltaTime);
            _camera.Update(GraphicsDevice, deltaTime);
            Input.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        public void MouseOver()
        {
            // Reset the color of the object that was being hovered over
            if (_mouseIsOver != null)
                _mouseIsOver.Sprite.Color = Color.White;

            // Get the object the mouse is hovering.
            _mouseIsOver = _hotel.GetObject(_camera.ScreenToWorld(Input.Instance.GetMousePos()));

            // If a mouseover object is found
            if (_mouseIsOver != null)
            {
                // Highlight the sprite.
                _mouseIsOver.Sprite.Color = Color.LightGreen;

                // When the left mouse is clicked
                if (Input.Instance.OnLeftMouseButtonRelease())
                {
                    if (!_DI.IsShowingInfo)
                    {
                        _wasSelected = _mouseIsOver;
                        _DI.ShowInformation(_mouseIsOver);
                    }
                    else
                    {
                        if (_wasSelected.Equals(_mouseIsOver))
                        {
                            _DI.HideInformation();
                        }
                        else
                        {
                            _wasSelected = _mouseIsOver;
                            _DI.HideInformation();
                            _DI.ShowInformation(_mouseIsOver);
                        }
                    }
                }
            }
            else
            {
                if(Input.Instance.OnLeftMouseButtonRelease())
                    _DI.HideInformation();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // GameObject Spritebatch
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _camera.TransformMatrix);

            _hotel.Draw(_spriteBatch, gameTime);

            // End the drawing on the spritebatch.
            _spriteBatch.End();

            // HUD Spritebatch
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            _DI.Draw(_spriteBatch, gameTime);

            // End the drawing on the spritebatch.
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

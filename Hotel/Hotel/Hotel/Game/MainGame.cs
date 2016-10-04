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
using HotelEvents;
using System.IO;
using Newtonsoft.Json;

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
        private Camera _closeUpCamera;
        private HotelObject _mouseIsOver;
        private HotelObject _wasSelected;
        private HotelEventListener _listener;
        private InformationWindow _informationWindow;
        private ConfigModel _config;

        private RenderTarget2D _renderTexture;
        private Background _background;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Hotel Simulation";
            IsMouseVisible = true;

            // Enable v-sync
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;

            _graphics.IsFullScreen = false;

            // Subscribe to the exiting event
            Exiting += MainGame_Exiting;

            HotelEventManager.Start();

            _listener = new EventListener();
            HotelEventManager.Register(_listener);

            // Disable the fixed time step, causes low frame rates on some computers.
            IsFixedTimeStep = false;

            // Load Settings
            try
            {
                using (StreamReader sr = new StreamReader(@"Config.cfg"))
                using (JsonReader jsonReader = new JsonTextReader(sr))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    // Instantiate an object of type model and fill it.
                    _config = jsonSerializer.Deserialize<ConfigModel>(jsonReader);

                    // Check if files exist
                    if (!File.Exists(_config.LayoutPath))
                    {
                        Console.WriteLine($"There is no layout file on the end of this path: {_config.LayoutPath}");
                        this.Exit();
                    }
                    

                    // Set other settings
                    HotelEventManager.HTE_Factor = _config.HTELength;
                }
            }
            catch
            {
                Console.WriteLine("Could not read 'Config.cfg', does it not exist?");
                this.Exit();
            }
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

            _renderTexture = new RenderTarget2D(GraphicsDevice, 200, 200, false, SurfaceFormat.Color, DepthFormat.None);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _informationWindow = new InformationWindow(Content);

            _hotel = new Hotel(Content, _config);
            _camera = new Camera(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            _camera.Controlable = true;
            _closeUpCamera = new Camera(200, 200);
            _background = new Background(Content, _camera);
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

            Input.Instance.Update(gameTime);

            // Check for mouseover
            MouseOver();

            _hotel.Update(deltaTime);
            _camera.Update(deltaTime);
            _closeUpCamera.Update(deltaTime);
            _background.Update(deltaTime);

            base.Update(gameTime);
        }

        private void MouseOver()
        {
            // Reset the color of the object that was being hovered over
            if (_mouseIsOver != null)
                _mouseIsOver.Sprite.Color = Color.White;

            // Get the object the mouse is hovering.
            _mouseIsOver = _hotel.GetObject(_camera.ScreenToWorld(Input.Instance.GetMousePos()));

            if(_wasSelected != null)
                _closeUpCamera.CamPosition = new Vector2(-_wasSelected.Position.X - _wasSelected.Sprite.DrawDestination.Width / 2, _wasSelected.Position.Y - _wasSelected.Sprite.DrawDestination.Height / 2);

            // If a mouseover object is found
            if (_mouseIsOver != null)
            {
                // Highlight the sprite.
                _mouseIsOver.Sprite.Color = Color.LightGreen;

                // When the left mouse is clicked
                if (Input.Instance.OnLeftMouseButtonRelease())
                {
                    // Check if the information window is already showing some data.
                    if (!_informationWindow.IsShowingInfo)
                    {
                        // Set the object as selected and show its information
                        _wasSelected = _mouseIsOver;
                        _informationWindow.ShowInformation(_mouseIsOver);
                    }
                    else
                    {
                        // Hide the information window
                        if (_wasSelected.Equals(_mouseIsOver))
                        {
                            _informationWindow.HideInformation();
                        }
                        else
                        {
                            // Update the information window
                            _wasSelected = _mouseIsOver;
                            _informationWindow.HideInformation();
                            _informationWindow.ShowInformation(_mouseIsOver);
                        }
                    }
                }
            }
            else
            {
                // Hide the information window
                if(Input.Instance.OnLeftMouseButtonRelease())
                    _informationWindow.HideInformation();
            }
        }

        /// <summary>
        /// Render hotel to a texture (for the closeup)
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        private void DrawToTarget(GameTime gameTime)
        {
            // Set the render target
            GraphicsDevice.SetRenderTarget(_renderTexture);

            // Clear
            GraphicsDevice.Clear(new Color(11, 83, 159));

            // GameObject Spritebatch
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _closeUpCamera.TransformMatrix);

            // Draw the hotel to the texture
            _hotel.Draw(_spriteBatch, gameTime);

            // End the drawing on the spritebatch.
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Draw the hotel to a render texture.
            DrawToTarget(gameTime);

            // Clear
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Hotel Spritebatch
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _camera.TransformMatrix);

            // Draw the objects in the scene
            _background.Draw(_spriteBatch, gameTime);
            _hotel.Draw(_spriteBatch, gameTime);

            // End the drawing on the spritebatch.
            _spriteBatch.End();

            // HUD Spritebatch
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            _informationWindow.RenderTexture = _renderTexture;
            _informationWindow.Draw(_spriteBatch, gameTime);

            // End the drawing on the spritebatch.
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

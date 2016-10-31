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
using Hotel.RoomsFactory;

namespace Hotel
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        public float HTE_Modifier { get; private set; }

        private SpriteBatch _spriteBatch;

        private Hotel _hotel;

        private Camera _camera;
        private Camera _closeUpCamera;

        private HotelObject _mouseIsOver;
        private HotelObject _wasSelected;

        private HotelEventListener _listener;
        private InformationWindow _informationWindow;

        private RenderTarget2D _renderTexture;
        private Sprite _background;

        public MainGame()
        {
            // General Service settings
            GraphicsDeviceManager graphicsDeviceManager = new GraphicsDeviceManager(this);

            // We are using the ServiceLocator pattern here!
            // Add the services to the static class.
            ServiceLocator.Add<ContentManager>(Content);
            ServiceLocator.Add<ConfigLoader>(new ConfigLoader(@"Config.cfg"));
            ServiceLocator.Add<ConfigLoader>(new ConfigLoader(@"Config.cfg"));
            // Service Locator is OFF LIMITS.

            // Set the directory for the texture pack
            Content.RootDirectory = ServiceLocator.Get<ConfigLoader>().GetConfig().TexturePack;

            // Window Settings
            Window.Title = "Hotel Simulation";

            IsMouseVisible = true;

            // Enable v-sync
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;

            // Set resolution
            graphicsDeviceManager.PreferredBackBufferHeight = 720;
            graphicsDeviceManager.PreferredBackBufferWidth = 1280;

            graphicsDeviceManager.IsFullScreen = false;

            // Subscribe to the exiting event
            Exiting += MainGame_Exiting;

            // Disable the fixed time step, causes low frame rates on some computers.
            IsFixedTimeStep = false;

            // Load config file.
            ConfigModel config = ServiceLocator.Get<ConfigLoader>().GetConfig();

            // Change settings related to HTE timespan.
            HotelEventManager.HTE_Factor = config.HTELength;
            HTE_Modifier = config.HTELength;
        }

        private void MainGame_Exiting(object sender, EventArgs e)
        {
            (_listener as EventListener).Exit();
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
            // Add your initialization logic here
            base.Initialize();

            _renderTexture = new RenderTarget2D(GraphicsDevice, 200, 200, false, SurfaceFormat.Color, DepthFormat.None);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Exit the simulation if there is no layout path.
            if (ServiceLocator.Get<ConfigLoader>().GetConfig().LayoutPath == null)
            {
                Environment.Exit(0);
            }

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create new information window
            _informationWindow = new InformationWindow();

            // Make a new hotel.
            _hotel = new Hotel();

            // Register components to the roomfactory
            _hotel.HotelBuilder.RoomFactory.RegisterComponent("Room", new GuestRoomFactoryComponent());
            _hotel.HotelBuilder.RoomFactory.RegisterComponent("Cinema", new CinemaFactoryComponent());
            _hotel.HotelBuilder.RoomFactory.RegisterComponent("Restaurant", new CafeFactoryComponent());
            _hotel.HotelBuilder.RoomFactory.RegisterComponent("Fitness", new FitnessFactoryComponent());
            _hotel.HotelBuilder.RoomFactory.RegisterComponent("Pool", new PoolFactoryComponent());

            _hotel.BuildHotel();

            // Create other things.
            _camera = new Camera(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            _camera.Controlable = true;
            _closeUpCamera = new Camera(200, 200);

            // Create the handlers and managers and start throwing events.
            HotelEventManager.Start();
            HotelEventHandler hotelEventHandler = new HotelEventHandler(_hotel);
            _listener = new EventListener(hotelEventHandler);
            HotelEventManager.Register(_listener);

            // Create a background.
            _background = new Sprite();
            _background.LoadSprite("Background");
            _background.SetPosition(new Point(0, 0));
            _background.SetSize(new Point(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));
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

            // Calculte the delta time.
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * HTE_Modifier;

            // Update input.
            Input.Instance.Update(gameTime);

            // Process user input.
            UserControlls();

            // Update the entire hotel.
            _hotel.Update(deltaTime);

            _camera.Update(deltaTime);
            _closeUpCamera.Update(deltaTime);

            base.Update(gameTime);
        }

        private void UserControlls()
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
        private void DrawToTarget(GameTime gameTime, RenderTarget2D target, Camera camera)
        {
            // Set the render target
            GraphicsDevice.SetRenderTarget(target);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            _background.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            // GameObject Spritebatch.
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.TransformMatrix);

            // Draw the hotel to the texture.
            _hotel.Draw(_spriteBatch, gameTime);

            // End the drawing on the spritebatch.
            _spriteBatch.End();

            // Default back to the default target.
            GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear
            GraphicsDevice.Clear(new Color(0, 0, 0));

            // Draw the hotel to a render texture.
            DrawToTarget(gameTime, _renderTexture, _closeUpCamera);
            // Draw the hotel to the default target.
            DrawToTarget(gameTime, null, _camera);
 
            // Draw the information window over all the other stuff.
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            _informationWindow.RenderTexture = _renderTexture;
            _informationWindow.Draw(_spriteBatch, gameTime);

            // End the drawing on the spritebatch.
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

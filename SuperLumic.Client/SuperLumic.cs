using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using SuperLumic.Abstracts;
using System;
using System.Collections.Generic;

namespace SuperLumic
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SuperLumic : Game
    {
        public static float CurrentPopupDrawlevel = 0.9f;
        public static float EndProjectileDrawLevel = .69f;
        public static float EndProjectileEffectDrawLevel = .59f;
        public static float EndRoomDrawLevel = .49f;
        public static float EndShipDrawLevel = .29f;
        public static float EndShipEffectDrawLevel = .19f;
        public static float EndStageLayer = .09f;
        public static float EndUIDrawLevel = .89f;
        public static float EndWeaponDrawLevel = .39f;
        public static double Height;
        public static SuperLumic Instance;
        public static Random Random = new Random();
        public static float ScreenLayer = .000001f;
        public static float StartingProjectileDrawLevel = .6f;
        public static float StartingProjectileEffectDrawLevel = .5f;
        public static float StartingRoomDrawLevel = .4f;
        public static float StartingShipDrawLevel = .2f;
        public static float StartingShipEffectDrawLevel = .1f;
        public static float StartingStageLayer = .01f;
        public static float StartingUIDrawLevel = .8f;
        public static float StartingWeaponDrawLevel = .3f;
        public static double Width;
        public GraphicsDeviceManager Graphics;
        public ScreenManager ScreenManager;
        public SpriteBatch SpriteBatch;
        private List<AbstractPopup> Popups = new List<AbstractPopup>();

        public SuperLumic()
        {
            Instance = this;
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ScreenManager = new ScreenManager();
            Components.Add(ScreenManager);
            Window.AllowUserResizing = true;
            SetFullScreen(false);
        }

        public void PopPopup()
        {
        }

        public void PushPopup(AbstractPopup Popup)
        {
        }

        public void SetFullScreen(bool value)
        {
            Graphics.HardwareModeSwitch = false;
            Graphics.IsFullScreen = value;
            Graphics.ApplyChanges();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            Width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            Graphics.BeginDraw();
            base.Draw(gameTime);
            Graphics.EndDraw();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        private static float NextFloat(float min, float max)
        {
            double val = (Random.NextDouble() * (max - min) + min);
            return (float)val;
        }
    }
}
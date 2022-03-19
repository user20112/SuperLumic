using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using SuperLumic.Abstracts;
using SuperLumic.Content;
using SuperLumic.Screens;
using SuperLumic.Service;
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
        public static float MouseLayer = .999f;
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
        public Tuple<int, int> MouseSize;
        public ScreenManager ScreenManager;
        public SpriteBatch SpriteBatch;
        private Texture2D Background; 
        private Texture2D BlackImage;
        public int PillarBoxXOffset = 0;
        public int PillarBoxYOffset = 0;
        private List<AbstractPopup> PopupsStack = new List<AbstractPopup>();
        private List<AbstractScreen> ScreenStack = new List<AbstractScreen>();

        public SuperLumic()
        {
            Instance = this;
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Components.Add(new MouseService());
            SetFullScreen(false);
            PushScreen(new StartScreen(this));
        }

        public void PopPopup()
        {
            if (ScreenStack.Count > 1)
            {//prevent from popping past last screen
                Components.Add(ScreenStack[ScreenStack.Count - 2]);
                Components.Remove(ScreenStack[ScreenStack.Count - 1]);
                ScreenStack[ScreenStack.Count - 1].Dispose();
                ScreenStack.RemoveAt(ScreenStack.Count - 1);
            }
        }

        public void PopScreen()
        {
            if (PopupsStack.Count > 0)
            {
                Components.Remove(PopupsStack[PopupsStack.Count - 1]);
                PopupsStack[PopupsStack.Count - 1].Dispose();
                PopupsStack.RemoveAt(PopupsStack.Count - 1);
            }
        }

        public void PushPopup(AbstractPopup Popup)
        {
            if (!Components.Contains(Popup))
            {
                Components.Add(Popup);
                PopupsStack.Add(Popup);
            }
        }

        public void PushScreen(AbstractScreen screen)
        {
            if (!Components.Contains(screen))
            {
                if (ScreenStack.Count > 0)
                {
                    Components.Remove(ScreenStack[ScreenStack.Count - 1]);
                }
                Components.Add(screen);
                ScreenStack.Add(screen);
            }
            else
            {
                throw new Exception("This screen instance is already here ?");
            }
        }

        public void SetFullScreen(bool value)
        {
            Graphics.HardwareModeSwitch = false;
            Graphics.IsFullScreen = value;
            Graphics.ApplyChanges();
        }

        internal void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle rectangle, Color tintColor, float rotation, Vector2 origin, SpriteEffects none, float adjustedLayerDepth)
        {
            destinationRectangle.X += PillarBoxXOffset;
            destinationRectangle.Y += PillarBoxYOffset;
            SpriteBatch.Draw(texture, destinationRectangle, rectangle, tintColor, rotation, origin, none, adjustedLayerDepth);
        }

        internal Tuple<double, double> GetMousePosition()
        {
            Vector2 Position = Mouse.GetState().Position.ToVector2();
            return new Tuple<double, double>(Position.X / Width, Position.Y / Height);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            RefreshPillarBoxes();
            MouseSize = new Tuple<int, int>(16, 16);
            SpriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.NonPremultiplied);
            Draw(BlackImage, new Rectangle(0, 0, (int)Width, (int)Height), new Rectangle(1, 1, 1, 1), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            base.Draw(gameTime);
            SpriteBatch.End();
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
            Background = TextureManager.Get("Backgrounds\\bg_lonelyBlueStar.png");
            BlackImage = TextureManager.Get("Black.png");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (AbstractScreen screen in ScreenStack)
            {
                screen.Dispose();
            }
            foreach (AbstractPopup popup in PopupsStack)
            {
                popup.Dispose();
            }
            TextureManager.OnKill();
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

        private void RefreshPillarBoxes()
        {
            int ActualHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            int ActualWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            Rectangle AspectRatio = new Rectangle(0, 0, 16, 9);
            Rectangle WindowRectangle = new Rectangle(0, 0, ActualWidth, ActualHeight);
            AspectRatio = AspectRatio.ScaleRectToFit(WindowRectangle);
            PillarBoxXOffset = (int)((WindowRectangle.Width - AspectRatio.Width) / 2);
            PillarBoxYOffset = (int)((WindowRectangle.Height - AspectRatio.Height) / 2);
            Height = AspectRatio.Height;
            Width = AspectRatio.Width;
        }
    }
}
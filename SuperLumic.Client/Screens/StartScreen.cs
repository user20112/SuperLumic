using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperLumic.Abstracts;
using SuperLumic.Content;
using System;

namespace SuperLumic.Screens
{
    public class StartScreen : AbstractScreen
    {
        private Texture2D Background;
        private Tuple<double, double> MousePosition;

        public StartScreen(SuperLumic game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Draw(Background);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            MousePosition = GetMousePosition();
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Background = TextureManager.Get("Backgrounds\\bg_lonelyBlueStar.png");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
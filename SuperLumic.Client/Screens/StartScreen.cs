using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperLumic.Abstracts;
using SuperLumic.Content;
using SuperLumic.Enums;
using SuperLumic.UIElements;
using System;
using System.Windows.Input;

namespace SuperLumic.Screens
{
    public class StartScreen : AbstractScreen
    {
        private Texture2D Background;
        
        public StartScreen(SuperLumic game) : base(game)
        {
        }
        public override void Draw(GameTime gameTime)
        {
            Draw(Background);
            base.Draw(gameTime);
        }
        public override void Init()
        {
            base.Init();
            AddChild(new ImageButton(this, 
                TextureManager.Get("ButtonImages\\Host2.png"),
                TextureManager.Get("ButtonImages\\Host1.png"),
                TextureManager.Get("ButtonImages\\Host3.png"),
                new Command(OnHostTapped),
                AlignmentEnum.Center,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .25,
                0,
                .5,
                .05,
                0,
                0
                ));
        }

        private void OnHostTapped(object sender, object e)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
        protected override void LoadContent()
        {
            Background = TextureManager.Get("Backgrounds\\z_bg_nebula4.png");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
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
            AddChild(new Image(this,
                TextureManager.Get("SuperLumicText.png"),
                TextureManager.Get("SuperLumicText.png"),
                AlignmentEnum.Center,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .5,
                .1,
                .5,
                .25,
                0,
                0));
            AddChild(new ImageButton(this,
                TextureManager.Get("ButtonImages\\Continue2.png"),
                TextureManager.Get("ButtonImages\\Continue1.png"),
                TextureManager.Get("ButtonImages\\Continue3.png"),
                new Command(OnContinueTapped),
                AlignmentEnum.End,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .9,
                .5,
                .1,
                .05,
                0,
                0
                ));
            AddChild(new ImageButton(this,
                TextureManager.Get("ButtonImages\\Join2.png"),
                TextureManager.Get("ButtonImages\\Join1.png"),
                TextureManager.Get("ButtonImages\\Join3.png"),
                new Command(OnJoinTapped),
                AlignmentEnum.End,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .9,
                .55,
                .1,
                .05,
                0,
                0
                ));
            AddChild(new ImageButton(this,
                TextureManager.Get("ButtonImages\\Host2.png"),
                TextureManager.Get("ButtonImages\\Host1.png"),
                TextureManager.Get("ButtonImages\\Host3.png"),
                new Command(OnHostTapped),
                AlignmentEnum.End,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .9,
                .6,
                .1,
                .05,
                0,
                0
                ));
            AddChild(new ImageButton(this,
                TextureManager.Get("ButtonImages\\Options2.png"),
                TextureManager.Get("ButtonImages\\Options1.png"),
                TextureManager.Get("ButtonImages\\Options3.png"),
                new Command(OnOptionsTapped),
                AlignmentEnum.End,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .9,
                .65,
                .1,
                .05,
                0,
                0
                ));
            AddChild(new ImageButton(this,
                TextureManager.Get("ButtonImages\\Stats2.png"),
                TextureManager.Get("ButtonImages\\Stats1.png"),
                TextureManager.Get("ButtonImages\\Stats3.png"),
                new Command(OnStatsTapped),
                AlignmentEnum.End,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .9,
                .7,
                .1,
                .05,
                0,
                0
                ));
            AddChild(new ImageButton(this,
                TextureManager.Get("ButtonImages\\Quit2.png"),
                TextureManager.Get("ButtonImages\\Quit1.png"),
                TextureManager.Get("ButtonImages\\Quit3.png"),
                new Command(OnCloseTapped),
                AlignmentEnum.End,
                AlignmentEnum.Center,
                AspectEnum.AspectFit,
                .9,
                .75,
                .1,
                .05,
                0,
                0
                ));
        }

        private void OnContinueTapped(object sender, object e)
        {
        }

        private void OnJoinTapped(object sender, object e)
        {
        }

        private void OnOptionsTapped(object sender, object e)
        {
        }

        private void OnStatsTapped(object sender, object e)
        {
        }

        private void OnCloseTapped(object sender, object e)
        {
            System.Environment.Exit(0);
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
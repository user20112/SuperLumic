using Microsoft.Xna.Framework;

namespace SuperLumic.Abstracts
{
    public class AbstractScreen : AbstractUIElement
    {
        protected AbstractScreen(object Parent) : base(Parent, 0, 0, 1, 1, SuperLumic.ScreenLayer, SuperLumic.ScreenLayer)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
using Microsoft.Xna.Framework;

namespace SuperLumic.Abstracts
{
    internal class AbstractScreen : AbstractUIElement
    {
        protected AbstractScreen(SuperLumic game, double x, double y, double width, double height) : base(game, x, y, width, height, SuperLumic.ScreenLayer, SuperLumic.ScreenLayer)
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
    }
}
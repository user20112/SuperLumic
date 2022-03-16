using Microsoft.Xna.Framework;

namespace SuperLumic.Abstracts
{
    public abstract class AbstractUI : AbstractUIElement
    {
        protected AbstractUI(object Parent, double x, double y, double width, double height) : base(Parent, x, y, width, height, SuperLumic.StartingUIDrawLevel, SuperLumic.EndUIDrawLevel)
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
using Microsoft.Xna.Framework;

namespace SuperLumic.Abstracts
{
    public abstract class AbstractStage : AbstractUIElement
    {
        protected AbstractStage(SuperLumic game, double x, double y, double width, double height) : base(game, x, y, width, height, SuperLumic.StartingStageLayer, SuperLumic.EndStageLayer)
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
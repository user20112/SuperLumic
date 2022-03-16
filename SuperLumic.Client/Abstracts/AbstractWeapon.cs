using Microsoft.Xna.Framework;

namespace SuperLumic.Abstracts
{
    public abstract class AbstractWeapon : AbstractUIElement
    {
        protected AbstractWeapon(object Parent, double x, double y, double width, double height) : base(Parent, x, y, width, height, SuperLumic.StartingWeaponDrawLevel, SuperLumic.EndWeaponDrawLevel)
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
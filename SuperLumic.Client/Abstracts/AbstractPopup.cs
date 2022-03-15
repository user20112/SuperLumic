﻿using Microsoft.Xna.Framework;

namespace SuperLumic.Abstracts
{
    public abstract class AbstractPopup : AbstractUIElement
    {
        private AbstractPopup(SuperLumic game, double x, double y, double width, double height, float PopupLayerStart, float PopupLayerEnd) : base(game, x, y, width, height, PopupLayerStart, PopupLayerEnd)
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
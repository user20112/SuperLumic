using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperLumic.Abstracts;
using SuperLumic.Enums;
using SuperLumic.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperLumic.UIElements
{
   public class Image : AbstractUIElement
    {
        Texture2D IdleTexture;
        Texture2D HoveredTexture;
        AlignmentEnum HorizontalAlignment;
        AlignmentEnum VerticalAlignment;
        AspectEnum Aspect;
        public Image(object parent, Texture2D IdleTexture, Texture2D HoveredTexture, AlignmentEnum HorizontalAlignment, AlignmentEnum VerticalAlignment, AspectEnum Aspect, double x, double y, double width, double height, float LayerS, float LayerE) : base(parent, x, y, width, height, LayerS, LayerE)
        {
            this.IdleTexture = IdleTexture;
            this.HoveredTexture = HoveredTexture;
            this.HorizontalAlignment = HorizontalAlignment;
            this.VerticalAlignment = VerticalAlignment;
            this.Aspect = Aspect;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (IsMouseWithinElement())
            {
                Draw(HoveredTexture, Aspect, HorizontalAlignment, VerticalAlignment);
            }
            else
            {
                Draw(IdleTexture, Aspect, HorizontalAlignment, VerticalAlignment);
            }
            base.Draw(gameTime);
        }
    }
}

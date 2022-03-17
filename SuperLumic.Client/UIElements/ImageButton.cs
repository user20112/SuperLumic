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
using System.Windows.Input;

namespace SuperLumic.UIElements
{
    public class ImageButton : AbstractUIElement
    {
        Texture2D IdleTexture;
        Texture2D HoveredTexture;
        Texture2D ClickedTexture;
        Command OnClick;
        AlignmentEnum HorizontalAlignment;
        AlignmentEnum VerticalAlignment;
        AspectEnum Aspect;
        public ImageButton(object parent, Texture2D IdleTexture, Texture2D HoveredTexture, Texture2D ClickedTexture, Command OnClick, AlignmentEnum HorizontalAlignment, AlignmentEnum VerticalAlignment, AspectEnum Aspect, double x, double y, double width, double height, float LayerS, float LayerE) : base(parent, x, y, width, height, LayerS, LayerE)
        {
            this.IdleTexture = IdleTexture;
            this.HoveredTexture = HoveredTexture;
            this.ClickedTexture = ClickedTexture;
            this.OnClick = OnClick;
            this.HorizontalAlignment = HorizontalAlignment;
            this.VerticalAlignment = VerticalAlignment;
            this.Aspect = Aspect;
        }
        public override void Update(GameTime gameTime)
        {
            if (IsMouseWithinElement())
            {
                if (MouseService.M1State == MouseButtonStateEnum.Clicked)
                {
                    OnClick?.Execute(this);
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (IsMouseWithinElement())
            {
                if (MouseService.M1State == MouseButtonStateEnum.Clicked || MouseService.M1State == MouseButtonStateEnum.Held)
                {
                    Draw(ClickedTexture,Aspect,HorizontalAlignment,VerticalAlignment);
                }
                else
                {
                    Draw(HoveredTexture, Aspect, HorizontalAlignment, VerticalAlignment);
                }
            }
            else
            {
                Draw(IdleTexture, Aspect, HorizontalAlignment, VerticalAlignment);
            }
            base.Draw(gameTime);
        }
    }
}

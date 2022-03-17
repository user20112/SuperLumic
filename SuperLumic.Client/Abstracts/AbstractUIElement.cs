using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperLumic.Enums;
using System;

namespace SuperLumic.Abstracts
{
    public abstract class AbstractUIElement : DrawableGameComponent
    {
        public new SuperLumic Game;
        public double Height;
        public object Parent;
        public double Width;
        public double X;
        public double Y;
        public double CurrentLayerWidth = 0;
        private float EndLayer;
        private float StartLayer;

        protected AbstractUIElement(object Parent, double x, double y, double width, double height, float startLayer, float endLayer) : base(SuperLumic.Instance)
        {
            Game = SuperLumic.Instance;
            this.Parent = Parent;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            StartLayer = startLayer;
            EndLayer = endLayer;
        }
        bool FirstUpdate = true;
        public virtual void Init()
        {
            FirstUpdate = false;
        }
        public double RealHeight
        {
            get
            {
                if (Parent is SuperLumic SuperLumic)
                {
                    return Height * SuperLumic.Height;
                }
                else if (Parent is AbstractUIElement element)
                {
                    return Height * element.RealHeight;
                }
                return 0;
            }
        }
        public void AddChild(AbstractUIElement element)
        {
            float AdjustedLayerDepth = (float)((EndLayer - StartLayer) * CurrentLayerWidth + StartLayer);
            element.StartLayer = AdjustedLayerDepth;
            element.EndLayer = AdjustedLayerDepth + .01f;
            element.CurrentLayerWidth = CurrentLayerWidth;
            CurrentLayerWidth += .001;
            SuperLumic.Instance.Components.Add(element);
        }
        public double RealWidth
        {
            get
            {
                if (Parent is SuperLumic SuperLumic)
                {
                    return Width * SuperLumic.Width;
                }
                else if (Parent is AbstractUIElement element)
                {
                    return Width * element.RealWidth;
                }
                return 0;
            }
        }
        public double ParentRealWidth
        {
            get
            {
                if (Parent is SuperLumic SuperLumic)
                {
                    return SuperLumic.Width;
                }
                else if (Parent is AbstractUIElement element)
                {
                    return element.RealWidth;
                }
                return 0;
            }
        }
        public double ParentRealHeight
        {
            get
            {
                if (Parent is SuperLumic SuperLumic)
                {
                    return SuperLumic.Height;
                }
                else if (Parent is AbstractUIElement element)
                {
                    return element.RealHeight;
                }
                return 0;
            }
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Vector2 Origin, float Rotation, Color TintColor, SpriteEffects effect, double LayerDepth, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            CurrentLayerWidth += .0001;
            float AdjustedLayerDepth = (float)((EndLayer - StartLayer) * LayerDepth + StartLayer);
            Rectangle DestinationRectangle = new Rectangle((int)(x * RealWidth + X * ParentRealWidth), (int)(y * RealHeight + Y * ParentRealHeight), (int)(width * RealWidth), (int)(height * RealHeight));
            Rectangle TextureRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Origin = new Vector2((float)(Origin.X * Width * RealWidth), (float)(Origin.Y * Height * RealHeight));
            if (Aspect == AspectEnum.AspectFit)
            {
                int Multiplyer = 100000;
                DestinationRectangle = new Rectangle((int)(DestinationRectangle.X * Multiplyer), (int)(DestinationRectangle.Y * Multiplyer), (int)(DestinationRectangle.Width * Multiplyer), (int)(DestinationRectangle.Height * Multiplyer));
                Rectangle NewDestinationRectangle = TextureRect.ScaleRectToFit(DestinationRectangle);
                NewDestinationRectangle.X = DestinationRectangle.X;
                NewDestinationRectangle.Y = DestinationRectangle.Y;
                int XOffset = 0;
                int YOffset = 0;
                switch (HorizontalAlignment)
                {
                    case AlignmentEnum.Start:
                        break;
                    case AlignmentEnum.Center:
                        XOffset = (DestinationRectangle.Width - NewDestinationRectangle.Width) / 2;
                        break;
                    case AlignmentEnum.End:
                        XOffset = (DestinationRectangle.Width - NewDestinationRectangle.Width);
                        break;
                }
                switch (VerticalAlignment)
                {
                    case AlignmentEnum.Start:
                        break;
                    case AlignmentEnum.Center:
                        YOffset = (DestinationRectangle.Height - NewDestinationRectangle.Height) / 2;
                        break;
                    case AlignmentEnum.End:
                        YOffset = (DestinationRectangle.Height - NewDestinationRectangle.Height);
                        break;
                }
                NewDestinationRectangle.X += XOffset;
                NewDestinationRectangle.Y += YOffset;
                Rectangle SecondDestinationRectangle = new Rectangle((int)((NewDestinationRectangle.X / Multiplyer)), (int)((NewDestinationRectangle.Y / Multiplyer)), (int)((NewDestinationRectangle.Width / Multiplyer)), (int)((NewDestinationRectangle.Height / Multiplyer)));
                DestinationRectangle = SecondDestinationRectangle;
            }
            Game.Draw(Texture, DestinationRectangle, TextureRect, TintColor, Rotation, Origin, SpriteEffects.None, AdjustedLayerDepth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor, SpriteEffects effect, double LayerDepth, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, LayerDepth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor, double LayerDepth, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, LayerDepth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor, SpriteEffects effect, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, CurrentLayerWidth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, CurrentLayerWidth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, Color.White, SpriteEffects.None, CurrentLayerWidth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void Draw(Texture2D Texture, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            Draw(Texture, 0, 0, 1, 1, new Vector2(0, 0), 0, Color.White, SpriteEffects.None, CurrentLayerWidth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            CurrentLayerWidth = 0;
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, Color.White, SpriteEffects.None, CurrentLayerWidth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Vector2 Origin, float Rotation, Color TintColor, SpriteEffects effect, double LayerDepth, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            CurrentLayerWidth += .0001;
            float AdjustedLayerDepth = (float)((EndLayer - StartLayer) * LayerDepth + StartLayer);
            Rectangle DestinationRectangle = new Rectangle((int)(x * RealWidth + X * ParentRealWidth), (int)(y * RealHeight + Y * ParentRealHeight), width, height);
            Origin = new Vector2((float)(Origin.X + X * RealWidth), (float)(Origin.Y + Y * RealHeight));
            Game.Draw(Texture, DestinationRectangle, new Rectangle(0, 0, Texture.Width, Texture.Height), TintColor, Rotation, Origin, SpriteEffects.None, AdjustedLayerDepth);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor, SpriteEffects effect, double LayerDepth, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, LayerDepth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor, double LayerDepth, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, LayerDepth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor, SpriteEffects effect, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, CurrentLayerWidth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor, AspectEnum Aspect = AspectEnum.AspectFit, AlignmentEnum HorizontalAlignment = AlignmentEnum.Center, AlignmentEnum VerticalAlignment = AlignmentEnum.Center)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, CurrentLayerWidth, Aspect, HorizontalAlignment, VerticalAlignment);
        }

        public override void Update(GameTime gameTime)
        {
            if (FirstUpdate)
            {
                Init();
            }
            base.Update(gameTime);
        }

        internal Tuple<double, double> GetMousePosition()
        {
            int X = (int)(this.X * SuperLumic.Width);
            int Y = (int)((1 - this.Y) * SuperLumic.Height);
            int Height = (int)RealHeight;
            int Width = (int)RealWidth;
            Vector2 Position = Mouse.GetState().Position.ToVector2();
            if (Position.X > X && Position.X < X + Width && Position.Y < Y && Position.Y > Y - Height)
            {
                return new Tuple<double, double>((Position.X - X) / Width, 1 + ((Position.Y - Y) / Height));
            }
            return new Tuple<double, double>(-1, -1);// not in the area, return -1,-1
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public bool IsMouseWithinElement()
        {
            if (GetMousePosition().Item1 == -1)
                return false;
            return true;
        }
    }
}
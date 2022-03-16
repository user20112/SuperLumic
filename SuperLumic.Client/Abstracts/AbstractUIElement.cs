using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private double CurrentLayerWidth = 0;
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

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Vector2 Origin, float Rotation, Color TintColor, SpriteEffects effect, double LayerDepth)
        {
            CurrentLayerWidth += .01;
            float AdjustedLayerDepth = (float)((EndLayer - StartLayer) * LayerDepth + StartLayer);
            Rectangle DestinationRectangle = new Rectangle((int)(x * RealWidth + X * RealWidth), (int)(y * RealHeight + Y * RealHeight), (int)(width * RealWidth), (int)(height * RealHeight));
            Origin = new Vector2((float)(Origin.X * Width * RealWidth + X * RealWidth), (float)(Origin.Y * Height * RealHeight + Y * RealHeight));
            Game.Draw(Texture, DestinationRectangle, new Rectangle(0, 0, Texture.Width, Texture.Height), TintColor, Rotation, Origin, SpriteEffects.None, AdjustedLayerDepth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor, SpriteEffects effect, double LayerDepth)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, LayerDepth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor, double LayerDepth)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, LayerDepth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor, SpriteEffects effect)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, CurrentLayerWidth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color TintColor)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, CurrentLayerWidth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, Color.White, SpriteEffects.None, CurrentLayerWidth);
        }

        public void Draw(Texture2D Texture)
        {
            Draw(Texture, 0, 0, 1, 1, new Vector2(0, 0), 0, Color.White, SpriteEffects.None, CurrentLayerWidth);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            CurrentLayerWidth = 0;
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, Color.White, SpriteEffects.None, CurrentLayerWidth);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Vector2 Origin, float Rotation, Color TintColor, SpriteEffects effect, double LayerDepth)
        {
            CurrentLayerWidth += .01;
            float AdjustedLayerDepth = (float)((EndLayer - StartLayer) * LayerDepth + StartLayer);
            Rectangle DestinationRectangle = new Rectangle((int)(x * RealWidth + X * RealWidth), (int)(y * RealHeight + Y * RealHeight), width, height);
            Origin = new Vector2((float)(Origin.X + X * RealWidth), (float)(Origin.Y + Y * RealHeight));
            Game.Draw(Texture, DestinationRectangle, new Rectangle(0, 0, Texture.Width, Texture.Height), TintColor, Rotation, Origin, SpriteEffects.None, AdjustedLayerDepth);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor, SpriteEffects effect, double LayerDepth)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, LayerDepth);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor, double LayerDepth)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, LayerDepth);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor, SpriteEffects effect)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, effect, CurrentLayerWidth);
        }

        public void DrawRawHeightWidth(Texture2D Texture, double x, double y, int width, int height, Color TintColor)
        {
            DrawRawHeightWidth(Texture, x, y, width, height, new Vector2(0, 0), 0, TintColor, SpriteEffects.None, CurrentLayerWidth);
        }

        public override void Update(GameTime gameTime)
        {
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

        private bool IsMouseWithinElement()
        {
            if (GetMousePosition().Item1 == -1)
                return false;
            return true;
        }
    }
}
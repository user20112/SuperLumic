using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperLumic.Abstracts
{
    public abstract class AbstractUIElement : DrawableGameComponent
    {
        public new SuperLumic Game;
        public double Height;
        public double Width;
        public double X;
        public double Y;
        private double CurrentLayerWidth = 0;
        private float EndLayer;
        private float StartLayer;

        protected AbstractUIElement(SuperLumic game, double x, double y, double width, double height, float startLayer, float endLayer) : base(game)
        {
            Game = game;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            StartLayer = startLayer;
            EndLayer = endLayer;
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Vector2 Origin, float Rotation, Color color, SpriteEffects effect, double LayerDepth)
        {
            CurrentLayerWidth += .01;
            float AdjustedLayerDepth = (float)((EndLayer - StartLayer) * LayerDepth + StartLayer);
            int X = (int)(this.X * SuperLumic.Width);
            int Y = (int)(this.Y * SuperLumic.Height);
            int Height = (int)(this.Height * SuperLumic.Height);
            int Width = (int)(this.Width * SuperLumic.Width);
            Rectangle DestinationRectangle = new Rectangle((int)(x * width + X), (int)(y * height + Y), (int)(width * Width), (int)(height * Height));
            Origin = new Vector2((float)(Origin.X * Width + X), (float)(Origin.Y * Height + Y));
            Game.SpriteBatch.Draw(Texture, DestinationRectangle, new Rectangle(0, 0, Texture.Width, Texture.Height), color, Rotation, Origin, SpriteEffects.None, AdjustedLayerDepth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color color, SpriteEffects effect, double LayerDepth)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, color, effect, LayerDepth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color color, double LayerDepth)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, color, SpriteEffects.None, LayerDepth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color color, SpriteEffects effect)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, color, effect, CurrentLayerWidth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height, Color color)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, color, SpriteEffects.None, CurrentLayerWidth);
        }

        public void Draw(Texture2D Texture, double x, double y, double width, double height)
        {
            Draw(Texture, x, y, width, height, new Vector2(0, 0), 0, Color.Transparent, SpriteEffects.None, CurrentLayerWidth);
        }

        public void Draw(Texture2D Texture)
        {
            Draw(Texture, 0, 0, 1, 1, new Vector2(0, 0), 0, Color.Transparent, SpriteEffects.None, CurrentLayerWidth);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            CurrentLayerWidth = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
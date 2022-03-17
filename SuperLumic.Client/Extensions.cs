using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperLumic
{
    public static class Extensions
    {
        public static Rectangle ScaleRectToFit(this Rectangle Rectangle, Rectangle ToScaleTo)
        {
            Rectangle rect = new Rectangle(Rectangle.Location, Rectangle.Size);
            double scale = (ToScaleTo.Width / rect.Width);
            if (rect.Height * scale > ToScaleTo.Height)
                scale = ToScaleTo.Height / rect.Height;
            rect.Width = (int)(rect.Width * scale);
            rect.Height = (int)(rect.Height * scale);
            return rect;
        }
    }
}

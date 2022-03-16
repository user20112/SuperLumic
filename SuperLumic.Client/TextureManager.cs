using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace SuperLumic.Content
{
    public class TextureManager
    {
        private static Dictionary<string, Texture2D> LoadedTextures = new Dictionary<string, Texture2D>();

        public static Texture2D Get(string Name)
        {
            if (!LoadedTextures.TryGetValue(Name, out Texture2D texture))
            {
                return Load(Name);
            }
            return texture;
        }

        public static Texture2D Load(string Name)
        {
            Texture2D texture = null;
            try
            {
                FileStream fileStream = new FileStream("Content/" + Name, FileMode.Open);
                texture = Texture2D.FromStream(SuperLumic.Instance.Graphics.GraphicsDevice, fileStream);
                LoadedTextures.Add(Name, texture);
                fileStream.Dispose();
            }
            catch
            {
            }
            return texture;
        }

        public static void OnKill()
        {
            foreach (Texture2D texture in LoadedTextures.Values)
            {
                texture.Dispose();
            }
        }

        public static void Unload(string Name)
        {
            if (LoadedTextures.TryGetValue(Name, out Texture2D texture))
            {
                LoadedTextures.Remove(Name);
                texture.Dispose();
            }
        }
    }
}
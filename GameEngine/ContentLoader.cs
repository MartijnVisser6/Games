using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public static class ContentLoader
    {
        public static ContentManager Content { get; set; }

        private static Dictionary<string, Texture2D> textures = new Dictionary<string,Texture2D>();

        public static Texture2D LoadSprite(string source)
        {
            Texture2D texture;

            if(!textures.Keys.Contains(source))
            {
                texture = Content.Load<Texture2D>(source);
                textures.Add(source, texture);
            }
            else
            {
                return textures[source];
            }

            return texture;
        }
    }
}

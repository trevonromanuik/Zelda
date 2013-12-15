using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public static class AssetManager
    {
        private static Dictionary<string, Texture2D> _textureList;
        private static Dictionary<string, SpriteFont> _fontList;
        private static ContentManager _content;

        public static void Initialize(ContentManager content)
        {
            _textureList = new Dictionary<string, Texture2D>();
            _fontList = new Dictionary<string, SpriteFont>();
            _content = content;
        }

        public static Texture2D LoadTexture(string name)
        {
            if (!_textureList.ContainsKey(name))
            {
                _textureList.Add(name, _content.Load<Texture2D>(name));
            }
            return _textureList[name];
        }

        public static SpriteFont LoadFont(string name)
        {
            if (!_fontList.ContainsKey(name))
            {
                _fontList.Add(name, _content.Load<SpriteFont>(name));
            }
            return _fontList[name];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Components
{
    class Sprite : Component
    {
        private Texture2D _texture;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Sprite(Texture2D texture, int width, int height)
        {
            _texture = texture;
            Width = width;
            Height = height;
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var position = GetComponent<Position>();
            if (position == null)
                return;

            spriteBatch.Draw(_texture, new Vector2(position.X - Width / 2, position.Y - Height / 2), Color.White);
        }
    }
}

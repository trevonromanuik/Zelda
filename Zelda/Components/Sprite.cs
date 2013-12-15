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
        public Vector2 Position { get; private set; }

        public Sprite(Texture2D texture, int width, int height, Vector2 position)
        {
            _texture = texture;
            Width = width;
            Height = height;
            Position = position;
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White);
        }

        public void Move(float x, float y)
        {
            Position = new Vector2(Position.X + x, Position.Y + y);
        }

        public void Teleport(Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
        }
    }
}

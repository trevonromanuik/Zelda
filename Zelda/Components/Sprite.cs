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
        public virtual int Width 
        {
            get { return _texture.Width; }
        }
        
        public virtual int Height 
        {
            get { return _texture.Height; }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
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

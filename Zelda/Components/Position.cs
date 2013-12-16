using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Zelda.Components
{
    class Position : Component
    {
        private Vector2 _position;

        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public Position(float x, float y)
        {
            _position = new Vector2(x, y);
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
        }

        public void Move(float x, float y)
        {
            _position = new Vector2(_position.X + x, _position.Y + y);
        }

        public void Teleport(float x, float y)
        {
            _position = new Vector2(x, y);
        }
    }
}

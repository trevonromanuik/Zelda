using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Move(float x, float y)
        {
            x += _position.X;
            y += _position.Y;

            var collision = GetComponent<Collision>();
            if (collision != null)
            {
                var sprite = GetComponent<Sprite>();
                if (sprite != null)
                {
                    Rectangle bounds = new Rectangle((int)(x - sprite.Width / 2), (int)(y - sprite.Width / 2), sprite.Width, sprite.Height);
                    var point = collision.CheckCollision(bounds);
                    x = point.X;
                    y = point.Y;
                }
                else
                {
                    var animatedSprite = GetComponent<AnimatedSprite>();
                    var animation = animatedSprite.Animation;
                    Rectangle bounds = new Rectangle((int)(x - animation.FrameWidth / 2), (int)(y - animation.FrameHeight / 2), animation.FrameWidth, animation.FrameHeight);
                    var point = collision.CheckCollision(bounds);
                    x = point.X;
                    y = point.Y;
                }
            }

            _position = new Vector2(x, y);
        }

        public void Teleport(float x, float y)
        {
            _position = new Vector2(x, y);
        }
    }
}

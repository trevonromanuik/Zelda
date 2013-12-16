using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Zelda.Events;

namespace Zelda.Components
{
    class PlayerInput : Component
    {
        private KeyboardState _keyState;
        private KeyboardState _prevKeyState;

        public override void Update(double gameTime)
        {
            var position = GetComponent<Position>();
            if (position == null)
                return;

            var sprite = GetComponent<AnimatedSprite>();
            if (sprite == null)
                return;

            _keyState = Keyboard.GetState();

            bool isUp = _keyState.IsKeyDown(Keys.Up);
            bool isDown = _keyState.IsKeyDown(Keys.Down);
            bool isLeft = _keyState.IsKeyDown(Keys.Left);
            bool isRight = _keyState.IsKeyDown(Keys.Right);

            var x = 0.0f;
            var y = 0.0f;

            if (isUp ^ isDown)
                y = isUp ? -1.5f : 1.5f;

            if (isLeft ^ isRight)
                x = isLeft ? -1.5f : 1.5f;

            if (x == 0 && y == 0)
            {
                sprite.Freeze();
            }
            else
            {
                sprite.Unfreeze();
                if (x != 0 ^ y != 0)
                {
                    if (x > 0) sprite.PlayAnimation("right");
                    else if (x < 0) sprite.PlayAnimation("left");
                    else if (y > 0) sprite.PlayAnimation("down");
                    else if (y < 0) sprite.PlayAnimation("up");
                }

                position.Move(x, y);
            }

            _prevKeyState = _keyState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

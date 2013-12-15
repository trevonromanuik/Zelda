using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

using Zelda.Events;

namespace Zelda.Components
{
    class PlayerInput : Component
    {
        public PlayerInput()
        {
            InputManager.FireNewInput += InputManager_FireNewInput;
        }

        void InputManager_FireNewInput(object sender, InputEventArgs e)
        {
            var sprite = GetComponent<Sprite>();
            if (sprite == null)
                return;

            var x = 0.0f;
            var y = 0.0f;

            switch (e.Input)
            {
                case Input.Up:
                    y = -1.5f;
                    break;

                case Input.Down:
                    y = 1.5f;
                    break;

                case Input.Left:
                    x = -1.5f;
                    break;

                case Input.Right:
                    x = 1.5f;
                    break;

                default:
                    return;
            }

            sprite.Move(x, y);
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zelda.Events;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class InputManager
    {
        private KeyboardState _keyState;
        private KeyboardState _prevKeyState;
        private static event EventHandler<InputEventArgs> _FireNewInput;

        private static Dictionary<Keys, Input> _mappings =
            new Dictionary<Keys, Input>
            {
                { Keys.Up, Input.Up },
                { Keys.Down, Input.Down },
                { Keys.Left, Input.Left },
                { Keys.Right, Input.Right },
                { Keys.Enter, Input.Enter },
                { Keys.A, Input.A },
                { Keys.S, Input.S },
            };

        public static event EventHandler<InputEventArgs> FireNewInput
        {
            add { _FireNewInput += value; }
            remove { _FireNewInput -= value; }
        }

        public void Update(double gameTime)
        {
            _keyState = Keyboard.GetState();
            foreach (var mapping in _mappings)
            {
                CheckKeyState(mapping.Key, mapping.Value);
            }
            _prevKeyState = _keyState;
        }

        private void CheckKeyState(Keys key, Input input)
        {
            if (_keyState.IsKeyDown(key))
            {
                if (_FireNewInput != null)
                {
                    _FireNewInput(this, new InputEventArgs(input));
                }
            }
        }
    }
}

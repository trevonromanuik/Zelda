using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Screens
{
    public abstract class Screen
    {
        protected ScreenManager _screenManager;

        public Screen(ScreenManager screenManager)
        {
            _screenManager = screenManager;
        }

        public virtual void Initialize() { }
        public virtual void Uninitialize() { }
        public abstract void LoadContent();
        public abstract void Update(double gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

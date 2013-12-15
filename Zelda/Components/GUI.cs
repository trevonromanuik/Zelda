using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Components
{
    class GUI : Component
    {
        private Texture2D _rupeeTexture;
        private Texture2D _heartTexture;
        private Texture2D _containerTexture;
        private Texture2D _backgroundTexture;
        private SpriteFont _font;

        public void LoadContent()
        {
            _rupeeTexture = AssetManager.LoadTexture("rupee_gui");
            _heartTexture = AssetManager.LoadTexture("heart_gui");
            _containerTexture = AssetManager.LoadTexture("container_gui");
            _backgroundTexture = AssetManager.LoadTexture("white_background");
            _font = AssetManager.LoadFont("Font_GUI");
        }

        public override void Update(double gameTime)
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_backgroundTexture, new Rectangle(0, 128, 160, 16), new Color(245, 245, 135));

            spritebatch.Draw(_containerTexture, new Rectangle(9, 130, 30, 12), Color.Black);
            spritebatch.DrawString(_font, "B", new Vector2(1, 129), Color.Black);

            spritebatch.Draw(_containerTexture, new Rectangle(47, 130, 30, 12), Color.Black);
            spritebatch.DrawString(_font, "A", new Vector2(40, 129), Color.Black);

            spritebatch.Draw(_rupeeTexture, new Rectangle(80, 130, 9, 9), Color.White);
            spritebatch.DrawString(_font, "999", new Vector2(80, 135), Color.Black);

            for (int n = 0; n < 3; n++)
            {
                spritebatch.Draw(_heartTexture, new Rectangle(100 + n * 10, 130, 9, 9), Color.White);
            }
        }
    }
}

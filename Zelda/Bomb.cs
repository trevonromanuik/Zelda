using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class Bomb : GameObject
    {
        private int x, y;
        private static Texture2D bombTexture;

        private float timer;

        public Bomb(int x, int y)
        {
            this.x = x;
            this.y = y;

            timer = 0;
        }

        public override void Update(double gameTime)
        {
            timer += (float)gameTime / 1000;
            if (timer > 5)
            {
                //IsActive = false;
                RoomManager.Instance.CurrentRoom.GameObjectManager.AddGameObject(new Explosion(x, y));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            double d = (-Math.Cos(2 * timer * timer) + 1) / 2;
            int i = (int)(d * 127);
            Color c = new Color(255, 255 - i, 255 - i);
            spriteBatch.Draw(bombTexture, new Vector2(x - bombTexture.Width / 2, y - bombTexture.Height / 2), c);
        }
    }
}

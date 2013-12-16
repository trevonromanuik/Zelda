using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Zelda.GameObjects;

namespace Zelda
{
    class Explosion : GameObject
    {
        private static Texture2D explosionTexture;

        private int x, y;

        private Animation explosionAnimation;
        private AnimationPlayer sprite;
        
        private float timer;

        public Explosion(int x, int y)
        {
            this.x = x;
            this.y = y;

            explosionAnimation = new Animation(explosionTexture, 16, 16, 0.1f, false);
            sprite.PlayAnimation(explosionAnimation);
        }

        public override void Update(double gameTime)
        {
            timer += (float)gameTime / 1000;
            if (timer > 0.6)
            {
                //IsActive = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //sprite.Draw(gameTime, spriteBatch, new Vector2(x, y), SpriteEffects.None);
        }
    }
}

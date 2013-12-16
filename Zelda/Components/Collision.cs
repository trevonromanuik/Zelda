using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Zelda.Maps;

namespace Zelda.Components
{
    class Collision : Component
    {
        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public bool CheckCollision(Rectangle bounds)
        {
            int tileX = (int)Math.Floor((float)bounds.Center.X / 16);
            int tileY = (int)Math.Floor((float)bounds.Center.Y / 16);

            int minI = Math.Max(0, tileY - 1);
            int maxI = Math.Min(Map.Instance.CurrentPanel.Tiles.GetLength(0) - 1, tileY + 1);

            int minJ = Math.Max(0, tileX - 1);
            int maxJ = Math.Min(Map.Instance.CurrentPanel.Tiles.GetLength(1) - 1, tileX + 1);

            for (int i = minI; i <= maxI; ++i)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    if (Map.Instance.CurrentPanel.Tiles[i, j] == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

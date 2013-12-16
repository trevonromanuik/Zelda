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
        public Vector2 CheckCollision(Vector2 position, int width, int height)
        {
            int minI = Math.Max(0, (int)Math.Floor((position.Y - (height / 2)) / 16));
            int maxI = Math.Min(Map.Instance.CurrentPanel.Tiles.GetLength(0) - 1, (int)Math.Floor((position.Y + (height / 2)) / 16));

            int minJ = Math.Max(0, (int)Math.Floor((position.X - (width / 2)) / 16));
            int maxJ = Math.Min(Map.Instance.CurrentPanel.Tiles.GetLength(1) - 1, (int)Math.Floor((position.X + (width / 2)) / 16));

            Rectangle bounds = new Rectangle((int)position.X - (width / 2), (int)position.Y - (height / 2), width, height);

            for (int i = minI; i <= maxI; ++i)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    if (Map.Instance.CurrentPanel.Tiles[i, j] == 1)
                    {
                        Rectangle tileBounds = new Rectangle(j * 16, i * 16, 16, 16);
                        Vector2 depth = bounds.GetIntersectionDepth(tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            if (absDepthY < absDepthX)
                            {
                                bounds.Y += (int)depth.Y;
                                position.Y += depth.Y;
                            }
                            else
                            {
                                bounds.X += (int)depth.X;
                                position.X += depth.X;
                            }
                        }
                    }
                }
            }

            return position;
        }

        public override void Update(double gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

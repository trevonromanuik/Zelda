using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    class Player : GameObject
    {
        private static Player instance;

        public static Player Instance 
        { 
            get { return instance ?? (instance = new Player()); } 
        }

        int x;
        int y;
        int width = 16;
        int height = 16;
        static Texture2D texture;

        private List<Equipment> equipment = new List<Equipment> { new EquipmentBomb() };
        private int currentEquipmentIndex = 0;

        public Equipment CurrentEquipment
        {
            get { return equipment[currentEquipmentIndex]; }
        }

        private KeyboardState prevState;

        public Player()
        {
            x = 120;
            y = 80;
        }

        public override void Update(double gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
                x -= 5;
            else if (state.IsKeyDown(Keys.Right))
                x += 5;

            if (state.IsKeyDown(Keys.Up))
                y -= 5;
            else if (state.IsKeyDown(Keys.Down))
                y += 5;

            HandleCollisions();

            if (state.IsKeyDown(Keys.Space) && !prevState.IsKeyDown(Keys.Space))
            {
                CurrentEquipment.Use(x, y);
            }

            if (x < 0)
            {
                RoomManager.Instance.ChangeRoom(-1, 0);
                x = 240;
            }
            else if (x > 240)
            {
                RoomManager.Instance.ChangeRoom(1, 0);
                x = 0;
            }
            else if (y < 0)
            {
                RoomManager.Instance.ChangeRoom(0, -1);
                y = 160;
            }
            else if (y > 160)
            {
                RoomManager.Instance.ChangeRoom(0, 1);
                y = 0;
            }

            prevState = state;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(x - width / 2, y - height / 2), Color.White);
        }

        private void HandleCollisions()
        {
            int tileX = (int)Math.Floor((float)x / 16);
            int tileY = (int)Math.Floor((float)y / 16);

            Rectangle playerBounds = new Rectangle(x - width / 2, y - width / 2, width, height);

            int minI = Math.Max(0, tileY - 1);
            int maxI = Math.Min(RoomManager.Instance.CurrentRoom.Tiles.GetLength(0) - 1, tileY + 1);

            int minJ = Math.Max(0, tileX - 1);
            int maxJ = Math.Min(RoomManager.Instance.CurrentRoom.Tiles.GetLength(1) - 1, tileX + 1);

            for (int i = minI; i <= maxI; ++i)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    if (RoomManager.Instance.CurrentRoom.Tiles[i, j] == 1)
                    {
                        Rectangle tileBounds = new Rectangle(j * 16, i * 16, 16, 16);
                        Vector2 depth = playerBounds.GetIntersectionDepth(tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            if (absDepthY < absDepthX)
                            {
                                y += (int)depth.Y;
                                playerBounds.Y += (int)depth.Y;
                            }
                            else
                            {
                                x += (int)depth.X;
                                playerBounds.X += (int)depth.X;
                            }
                        }
                    }
                }
            }
        }
    }
}

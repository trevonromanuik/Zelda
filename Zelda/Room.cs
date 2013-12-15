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
    class Room
    {
        private Color color;
        public GameObjectManager GameObjectManager { get; set; }
        public int[,] Tiles = new int[,] {
            {1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        };
        private static Texture2D wallTexture;

        public Room(Color color)
        {
            this.color = color;
            GameObjectManager = new GameObjectManager();
            GameObjectManager.AddGameObject(Player.Instance);
        }

        public void LoadContent(ContentManager content)
        {
            wallTexture = content.Load<Texture2D>("wall");
        }

        public void Update(double gameTime)
        {
            //KeyboardState state = Keyboard.GetState();
            //if (state.IsKeyDown(Keys.Left))
            //    RoomManager.Instance.ChangeRoom(-1, 0);
            //else if (state.IsKeyDown(Keys.Right))
            //    RoomManager.Instance.ChangeRoom(1, 0);
            //else if (state.IsKeyDown(Keys.Up))
            //    RoomManager.Instance.ChangeRoom(0, -1);
            //else if (state.IsKeyDown(Keys.Down))
            //    RoomManager.Instance.ChangeRoom(0, 1);

            GameObjectManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(color);

            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    if (Tiles[i,j] == 1)
                        spriteBatch.Draw(wallTexture, new Vector2(16 * j, 16 * i), Color.White);
                }
            }

            GameObjectManager.Draw(spriteBatch);
        }
    }
}

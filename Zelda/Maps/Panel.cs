using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Zelda.Components;
using Zelda.GameObjects;

namespace Zelda.Maps
{
    class Panel
    {
        private GameObjectManager _gameObjectManager;
        public int[,] Tiles;

        private Texture2D _wallTexture;

        public Panel(int[,] tiles)
        {
            Tiles = tiles;
            _gameObjectManager = new GameObjectManager();

            _wallTexture = AssetManager.LoadTexture("wall");

            _gameObjectManager.AddGameObject(Player.Instance);
        }

        public void Update(double gameTime)
        {
            _gameObjectManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    if (Tiles[i, j] == 1)
                        spriteBatch.Draw(_wallTexture, new Vector2(16 * j, 16 * i), Color.White);
                }
            }

            _gameObjectManager.Draw(spriteBatch);
        }
    }
}

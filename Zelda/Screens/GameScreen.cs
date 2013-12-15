using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

using Zelda.Components;
using Zelda.GameObjects;

namespace Zelda.Screens
{
    class GameScreen : Screen
    {
        private GameObjectManager _gameObjectManager;

        public GameScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            _gameObjectManager = new GameObjectManager();
        }

        public override void LoadContent()
        {
            var player = new GameObjects.Player();
            
            var gui = new GUI();
            gui.LoadContent();

            player.AddComponent(gui);

            _gameObjectManager.AddGameObject(player);
        }

        public override void Update(double gameTime)
        {
            _gameObjectManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _gameObjectManager.Draw(spriteBatch);
        }
    }
}

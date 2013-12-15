using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Zelda.Components;

namespace Zelda.GameObjects
{
    class Player : GameObject
    {
        public Player()
        {
            Id = "player";

            AddComponent(new Sprite(AssetManager.LoadTexture("player"), 16, 16, new Vector2(120, 80)));
            AddComponent(new PlayerInput());
        }
    }
}

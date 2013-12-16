using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Zelda.Components;
using Zelda.Maps;

namespace Zelda.GameObjects
{
    class Player : GameObject
    {
        private static Player _instance;
        public static Player Instance
        {
            get { return _instance ?? (_instance = new Player()); }
        }

        public Player()
        {
            Id = "player";

            AddComponent(new Position(80, 80));

            AnimatedSprite sprite = new AnimatedSprite();
            sprite.AddAnimation("up", new Animation(AssetManager.LoadTexture("link_up"), 12, 15, 0.2f, true));
            sprite.AddAnimation("down", new Animation(AssetManager.LoadTexture("link_down"), 13, 16, 0.2f, true));
            sprite.AddAnimation("left", new Animation(AssetManager.LoadTexture("link_left"), 14, 16, 0.2f, true));
            sprite.AddAnimation("right", new Animation(AssetManager.LoadTexture("link_right"), 14, 16, 0.2f, true));
            sprite.PlayAnimation("down");
            AddComponent(sprite);

            AddComponent(new PlayerInput());
            AddComponent(new Collision());
        }

        public override void Update(double gameTime)
        {
            base.Update(gameTime);
            var position = GetComponent<Position>();
            if (position.Y < 0) 
            { 
                Map.Instance.ChangePanel(Direction.Up); 
                position.Teleport(position.X, 128); 
            }
            else if (position.Y > 128)
            {
                Map.Instance.ChangePanel(Direction.Down);
                position.Teleport(position.X, 0); 
            }
            else if (position.X < 0)
            {
                Map.Instance.ChangePanel(Direction.Left);
                position.Teleport(160, position.Y); 
            }
            else if (position.X > 160)
            {
                Map.Instance.ChangePanel(Direction.Right);
                position.Teleport(0, position.Y); 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class EquipmentBomb : Equipment
    {
        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("bomb_equipment");
        }

        public override void Use(int x, int y)
        {
            RoomManager.Instance.CurrentRoom.GameObjectManager.AddGameObject(new Bomb(x, y));
        }
    }
}

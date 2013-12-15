using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public abstract class Equipment
    {
        public Texture2D Texture { get; set; }

        public abstract void LoadContent(ContentManager content);

        public abstract void Use(int x, int y);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Components
{
    abstract class Component
    {
        private GameObject _gameObject;

        public void Initialize(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public string OwnerId
        {
            get { return _gameObject == null ? string.Empty : _gameObject.Id; }
        }

        public void Remove()
        {
            _gameObject.RemoveComponent(this);
        }

        public T GetComponent<T>() where T : Component
        {
            return _gameObject == null ? null : _gameObject.GetComponent<T>();
        }

        public abstract void Update(double gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

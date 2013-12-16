using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zelda.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.GameObjects
{
    class GameObject
    {
        public string Id { get; set; }
        private readonly List<Component> _components;

        public GameObject()
        {
            _components = new List<Component>();
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)_components.Find(x => IsOfType<T>(x));
        }

        private bool IsOfType<T>(Component component) where T : Component
        {
            Type cType = typeof(Component);
            Type tType = typeof(T);

            Type type = component.GetType();
            while (type != cType)
            {
                if (type == tType)
                    return true;

                type = type.BaseType;
            }
            return false;
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);
            component.Initialize(this);
        }

        public void AddComponents(List<Component> components)
        {
            _components.AddRange(components);
            foreach (var component in components)
            {
                component.Initialize(this);
            }
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }

        public virtual void Update(double gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}

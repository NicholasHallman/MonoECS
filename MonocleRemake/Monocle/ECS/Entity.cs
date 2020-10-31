using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ECS
{
    class Entity
    {
        private ComponentManager componentManager;
        private Dictionary<Type, int> components;
        public Guid id { get; private set; }
        public Entity()
        {
            componentManager = ComponentManager.Instance();
            components = new Dictionary<Type, int>();
            id = new Guid();
        }
        
        public Entity AddComponent<T>() where T : Component
        {
            int position = componentManager.Add<T>(this);
            components[typeof(T)] = position;

            return this;
        }

        public void RemoveComponent<T>() where T : Component
        {
            T component = (T)Activator.CreateInstance(typeof(T));
            Type t = component.GetType();

            if (!components.ContainsKey(t))
            {
                throw new Exception("Entity does not have a component of type " + t.ToString());
            }

            int position = components[t];

            componentManager.Remove(new ComponentReference()
            {
                index = position,
                ComponentType = t
            });
            components.Remove(t);
        }

        public void RemoveAllComponents()
        {
            foreach(Type component in components.Keys)
            {
                int index = components[component];
                ComponentReference componentReference = new ComponentReference()
                {
                    index = index,
                    ComponentType = component
                };
                componentManager.Remove(componentReference);
            }
        }

        public T GetComponent<T>() where T: Component
        {
            T component = (T)Activator.CreateInstance(typeof(T));
            Type t = component.GetType();

            if( !components.ContainsKey(t))
            {
                return null;
            }

            ComponentReference cr = new ComponentReference()
            {
                index = components[t],
                ComponentType = t
            };
            return componentManager.Get(cr) as T;
        }

        public Type[] GetAllComponents()
        {
            return components.Keys.ToArray();
        }

        public void ReassignComponent(Type component, int newIndex)
        {
            components[component] = newIndex;
        }
    }
}
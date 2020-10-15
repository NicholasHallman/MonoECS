using System;
using System.Collections.Generic;
using System.Dynamic;
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
            T component = (T)Activator.CreateInstance(typeof(T));
            Type t = component.GetType();
            component.SetEntity(this);

            if (components.ContainsKey(t))
            {
                throw new Exception("Entity already has a component of type " + t.ToString());
            }

            int position = componentManager.Add(component);
            components[component.GetType()] = position;

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

        public T GetComponent<T>() where T: Component
        {
            T component = (T)Activator.CreateInstance(typeof(T));
            Type t = component.GetType();


            if( !components.ContainsKey(t))
            {
                throw new Exception("Entity does not have a component of type " + t.ToString());
            }

            ComponentReference cr = new ComponentReference()
            {
                index = components[t],
                ComponentType = t
            };
            return componentManager.Get(cr) as T;
        }
    }
}
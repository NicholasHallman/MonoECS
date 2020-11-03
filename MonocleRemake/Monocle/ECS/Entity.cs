using Microsoft.Xna.Framework;
using Monocle.ECS;
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
        private Archetypes archetypes;
        public Guid id { get; private set; }
        public int archetypeId = -1;
        public Entity()
        {
            componentManager = ComponentManager.Instance();
            components = new Dictionary<Type, int>();
            archetypes = Archetypes.Instance();
            id = new Guid();
        }
        
        public Entity AddComponent<T>() where T : Component
        {
            Type[] oldComponents = GetAllComponents();
            T component = (T)Activator.CreateInstance(typeof(T));
            component.SetEntity(this);
            int position = componentManager.Add(component);
            components[typeof(T)] = position;
            archetypes.ChangeArchetype(this, oldComponents);
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
            Type[] oldComponents = GetAllComponents();
            components.Remove(t);
            archetypes.ChangeArchetype(this, oldComponents);

        }

        public void RemoveAllComponents()
        {
            Type[] oldComponents = GetAllComponents();
            foreach (Type component in components.Keys)
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
using Monocle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ECS

{
    public struct ComponentReference
    {
        public Type ComponentType;
        public int index;
    }
    class ComponentManager
    {
        private Dictionary<Type, ComponentStorage> stores;
        public static ComponentManager instance;

        public ComponentManager()
        {
            stores = new Dictionary<Type, ComponentStorage>();
        }

        public static ComponentManager Instance()
        {
            if(instance == null)
            {
                instance = new ComponentManager();
            }
            return instance;
        }

        public int Add<T>(Entity e) where T : Component
        {
            Type type = typeof(T);
            if (!stores.ContainsKey(type))
            {
                stores[type] = new ComponentStorage();
            }
            return stores[type].Add<T>(e);
        }

        public void Remove(ComponentReference cr)
        {
            stores[cr.ComponentType].Remove(cr.index);
        }

        public Component Get(ComponentReference cr)
        {
            return stores[cr.ComponentType].Get(cr.index);
        }

        public List<Entity> GetEntitiesWithComponent(Type component)
        {
            if(stores.ContainsKey(component))
            {
                return stores[component].GetEntities();
            }
            return new List<Entity>();
        }
    }
}

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
        private Dictionary<Type, VectorisedStorage<Component>> stores;
        public static ComponentManager instance;

        public ComponentManager()
        {
            stores = new Dictionary<Type, VectorisedStorage<Component>>();
        }

        public static ComponentManager Instance()
        {
            if(instance == null)
            {
                instance = new ComponentManager();
            }
            return instance;
        }

        public int Add(Component c)
        {
            Type type = c.GetType();
            if (!stores.ContainsKey(type))
            {
                stores[type] = new VectorisedStorage<Component>();
            }
            return stores[type].Add(c);
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

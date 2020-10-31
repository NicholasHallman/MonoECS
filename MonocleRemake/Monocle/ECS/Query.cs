using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Windows.Foundation.Metadata;

namespace ECS
{
    class Query
    {
        Type[] queryComponents;
        Type[] excludeComponents;
        ComponentManager componentManager;
        string mode;
        public bool useCache;
        private Query(Type[] components)
        {
            queryComponents = components;
            componentManager = ComponentManager.Instance();
            useCache = false;
        }

        private Query(Type[] components, string mode = "All")
        {
            this.mode = mode;
            queryComponents = components;
            componentManager = ComponentManager.Instance();
            useCache = false;
        }

        public static Query All(Type[] components)
        {
            return new Query(components);
        }

        public static Query Only(Type[] components)
        {
            return new Query(components, "Only");
        }

        public Query Not(Type[] exclude)
        {
            this.excludeComponents = exclude;
            return this;
        }

        private bool AreEqualTypeArrays(Type[] a, Type[] b)
        {
            Dictionary<Type, bool> found = new Dictionary<Type, bool>();
            Array.ForEach(a, t => found.Add(t, true));
            foreach(Type t in b)
            {
                if (!found.ContainsKey(t)) return false;
            }
            return true;
        }

        private Entity[] RunOnly()
        {
            HashSet<Entity> foundEntities = new HashSet<Entity>();
            foreach(Type componentType in queryComponents)
            {
                List<Entity> entities = componentManager.GetEntitiesWithComponent(componentType);
                IEnumerable<Entity> filtered = entities.Where(entity => {
                    Type[] entityComponents = entity.GetAllComponents();
                    return AreEqualTypeArrays(entityComponents, queryComponents);
                });
                foundEntities.UnionWith(filtered);
            }
            return foundEntities.ToArray();
        }

        private Entity[] RunAll()
        {
            HashSet<Entity> foundEntities = new HashSet<Entity>();
            foreach (Type componentType in queryComponents)
            {
                List<Entity> entities = componentManager.GetEntitiesWithComponent(componentType);
                if (foundEntities.Count == 0)
                {
                    foundEntities.UnionWith(entities);
                }
                else
                {
                    foundEntities.IntersectWith(entities);
                }
            }
            if (excludeComponents != null)
            {
                foreach (Type componentType in excludeComponents)
                {
                    List<Entity> entities = componentManager.GetEntitiesWithComponent(componentType);
                    foundEntities.ExceptWith(entities);
                }
            }
            return foundEntities.ToArray();
        }


        public Entity[] Run()
        {
            if (mode == "Only")
            {
                return RunOnly();
            } else
            {
                return RunAll();
            }
            
        }
    }

}

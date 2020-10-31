using System;
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
        public bool useCache;
        public Query(Type[] components)
        {
            queryComponents = components;
            componentManager = ComponentManager.Instance();
            useCache = false;
        }

        public Query(Type[] components, Type[] exclude)
        {
            queryComponents = components;
            excludeComponents = exclude;
            componentManager = ComponentManager.Instance();
            useCache = false;
        }

        public Entity[] All()
        {
            HashSet<Entity> foundEntities = new HashSet<Entity>();
            foreach(Type componentType in queryComponents){
                List<Entity> entities = componentManager.GetEntitiesWithComponent(componentType);
                if(foundEntities.Count == 0)
                {
                    foundEntities.UnionWith(entities);
                } 
                else
                {
                    foundEntities.IntersectWith(entities);
                }
            }
            if(excludeComponents != null)
            {
                foreach(Type componentType in excludeComponents)
                {
                    List<Entity> entities = componentManager.GetEntitiesWithComponent(componentType);
                    foundEntities.ExceptWith(entities);
                }
            }
            return foundEntities.ToArray();
        }
    }

}

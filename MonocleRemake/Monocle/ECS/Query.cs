using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ECS
{
    class Query
    {
        Type[] queryComponents;
        ComponentManager componentManager;
        public bool useCache;
        public Query(Type[] components)
        {
            queryComponents = components;
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
            return foundEntities.ToArray();
        }
    }

}

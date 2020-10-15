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
        public Query(Type[] components)
        {
            queryComponents = components;
            componentManager = ComponentManager.Instance();
        }

        public Entity[] All()
        {
            HashSet<Entity> foundEntities = new HashSet<Entity>();
            foreach(Type componentType in queryComponents){
                List<Entity> entities = componentManager.GetEntitiesWithComponent(componentType);
                foundEntities.UnionWith(entities);
            }

            return foundEntities.ToArray();
        }
    }

}

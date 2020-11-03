using ECS;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Monocle.ECS
{
    class Archetypes
    {
        private static Archetypes instance;
        private Dictionary<string, VectorisedStorage<Entity>> stores;
        private Archetypes()
        {
            stores = new Dictionary<string, VectorisedStorage<Entity>>();
        }

        static public Archetypes Instance()
        {
            if (instance != null) return instance;
            instance = new Archetypes();
            return instance;
        }

        private string GetComponentString(Type[] components)
        {
            ImmutableSortedSet<string> sortedComponents = components.Select(comp => comp.ToString()).ToImmutableSortedSet();
            return string.Join(',', sortedComponents);
        }

        private string[] GetComponentsInString(string componentString)
        {
            return componentString.Split(',');
        }

        public void AddEntity(Entity e)
        {
            string key = GetComponentString(e.GetAllComponents());
            int index;
            if (stores.ContainsKey(key))
            {
                index = stores[key].Add(e);
            } else
            {
                stores.Add(key, new VectorisedStorage<Entity>());
                index = stores[key].Add(e);
            }
            e.archetypeId = index;
        }

        public void ChangeArchetype(Entity e, Type[] oldTypes)
        {
            string oldKey = GetComponentString(oldTypes);
            if(e.archetypeId != -1 && oldKey != "")
            {
                stores[oldKey].Remove(e.archetypeId);
            }
            AddEntity(e);
        }

        public void Remove(Entity e)
        {
            string key = GetComponentString(e.GetAllComponents());
            stores[key].Remove(e.archetypeId);
        }

        private IEnumerable<string> MatchingKeys(string[] keys, string[] search)
        {
            return keys.Where(key =>
            {
                string[] comps = GetComponentsInString(key);
                var result = search.Intersect(comps);
                return result.Count() == search.Length;
            });
        }
        
        public Entity[] AllEntitiesOf(Type[] includes, Type[] excludes)
        {
            List<Entity> entities = new List<Entity>();

            string[] search = GetComponentsInString(GetComponentString(includes));
            var keys = MatchingKeys(stores.Keys.ToArray(), search);

            if(excludes != null)
            {
                string[] ignore = GetComponentsInString(GetComponentString(excludes));
                var excludeStrings = MatchingKeys(keys.ToArray(), ignore);
                keys = keys.Where(key => !excludeStrings.Contains(key));
            }
            
            foreach(string key in keys)
            {
                Entity[] es = stores[key].Dump();
                entities.AddRange(es);
            }

            return entities.ToArray();
        }

        public Entity[] OnlyEntitiesOf(Type[] components)
        {
            string search = GetComponentString(components);
            if (stores.ContainsKey(search)) return stores[search].Dump();
            return new Entity[0];
        }
    }
}

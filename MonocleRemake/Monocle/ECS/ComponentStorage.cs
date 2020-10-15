using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ECS
{
    class ComponentStorage
    {
        private Component[] store;
        private List<int> empty;
        private int tail;
        public ComponentStorage()
        {
            store = new Component[100];
            empty = new List<int>();
            tail = 0;
        }
        
        public int Add(Component component)
        {
            if(empty.Count > 0)
            {
                int index = empty[0];
                store[index] = component;
                empty.RemoveAt(0);
                return index;
            } 
            else
            {
                if(tail >= store.Length)
                {
                    Array.Resize(ref store, store.Length + 50);
                }
                store[tail] = component;
                tail++;
                return tail - 1;
            }
        }

        public void Remove(int index)
        {
            store[index] = null;
            empty.Add(index);
        }

        public Component Get(int index)
        {
            return store[index];
        }

        public Component[] Dump()
        {
            return store;
        }

        public List<int> Empty()
        {
            return empty;
        }

        public List<Entity> GetEntities()
        {
            List<Entity> entities = new List<Entity>();
            for(int i = 0; i < tail; i++)
            {
                if(!empty.Contains(i))
                {
                    entities.Add(store[i].GetEntity());
                }
            }

            return entities;
        }
    }
}

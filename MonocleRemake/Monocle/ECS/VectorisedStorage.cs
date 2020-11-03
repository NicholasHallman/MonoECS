using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using Windows.ApplicationModel.Calls;
using Windows.UI.Xaml.Media.Animation;

namespace ECS
{
    class VectorisedStorage<U>
    {
        private U[] store;
        private int tail;
        public VectorisedStorage()
        {
            store = new U[100];
            tail = 0;
        }

        public int Add(U u)
        {

            if (tail >= store.Length)
            {
                Array.Resize(ref store, store.Length * 2);
            }
            store[tail] = u;
            tail++;
            return tail - 1;
            
        }

        public void Remove(int index)
        {
            if(index != tail - 1 && tail > 0)
            {
                store[index] = store[tail - 1];
                Type t = store[index].GetType();
                if (typeof(U) == typeof(Component))
                {
                    ((dynamic)store[index]).GetEntity().ReassignComponent(t, index);
                }
                if(typeof(U) == typeof(Entity))
                {
                    ((dynamic)store[index]).archetypeId = index;
                }
                tail -= 1;
            } 
            else if(index == tail - 1)
            {
                tail -= 1;
            }
            else
            {
                Debug.Write("Couldn't Swap index: " + index + " tail: " + tail);
            }
        }

        public U Get(int index)
        {
            return store[index];
        }
        
        public U[] Dump()
        {
            int diff = store.Length - tail;
            U[] copyStore = new U[store.Length - diff];
            Array.Copy(store, copyStore, copyStore.Length);
            return copyStore;
        }

        public List<Entity> GetEntities()
        {
            if (typeof(U) != typeof(Component)) return new List<Entity>();
            List<Entity> entities = new List<Entity>();
            for(int i = 0; i < tail; i++)
            {
                if(typeof(U) == typeof(Component))
                {
                    entities.Add(((dynamic)store[i]).GetEntity());
                }
            }

            return entities;
        }
    }
}

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
    class ComponentStorage
    {
        private Component[] store;
        private int tail;
        public ComponentStorage()
        {
            store = new Component[100];
            tail = 0;
        }

        public int Add<T>(Entity e) where T : Component
        {

            if (tail >= store.Length)
            {
                Array.Resize(ref store, store.Length * 2);
            }
            store[tail] = (T)Activator.CreateInstance(typeof(T));
            store[tail].SetEntity(e);
            tail++;
            return tail - 1;
            
        }

        public void Remove(int index)
        {
            if(index != tail - 1 && tail > 0)
            {
                store[index] = store[tail - 1];
                Type t = store[index].GetType();
                store[index].GetEntity().ReassignComponent(t, index);
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

        public Component Get(int index)
        {
            return store[index];
        }
        
        public Component[] Dump()
        {
            return store;
        }

        public List<Entity> GetEntities()
        {
            List<Entity> entities = new List<Entity>();
            for(int i = 0; i < tail; i++)
            {
                entities.Add(store[i].GetEntity());
            }

            return entities;
        }
    }
}

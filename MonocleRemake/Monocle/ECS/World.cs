using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ECS
{
    class World
    {
        private List<Entity> entities;
        private ComponentManager componentManager;
        private Dictionary<string, List<Service>> services;
        private bool cacheEmpty = true;

        public static SpriteBatch spriteBatch;
        public static ContentManager content;
        public static GraphicsDevice GraphicsDevice;

        public World()
        {
            entities = new List<Entity>();
            componentManager = ComponentManager.Instance();
            services = new Dictionary<string, List<Service>>();
            services["default"] = new List<Service>();
        }

        public Entity CreateEntity()
        {
            Entity entity = new Entity();
            entities.Add(entity);
            RemoveServiceCache();
            return entity;
        }

        public void RemoveEntity(Entity e)
        {
            entities.Remove(e);
            e.RemoveAllComponents();
            cacheEmpty = true;
        }

        public void RemoveServiceCache()
        {
            if (cacheEmpty) return;
            foreach(string key in services.Keys)
            {
                foreach(Service service in services[key])
                {
                    service.IgnoreCache();
                }
            }
            cacheEmpty = true;
            return;
        }

        public void AddServiceGroup(string name){
            if (services.ContainsKey(name)) throw new Exception("Group exists");
            services[name] = new List<Service>();
        }

        public World AddService(Service service, string group = "default")
        {
            if (!services.ContainsKey(group)) throw new Exception("Group does not exists");

            services[group].Add(service);
            return this;
        }

        public World RemoveService(Service service, string group)
        {
            services[group].Remove(service);
            return this;
        }

        public void Run(string group = "default")
        {
            foreach( Service service in services[group])
            {
                service.Run(this);
            }
            cacheEmpty = false;
        }

        public List<Entity> GetEntitiesWithComponent<T>()
        {
            return componentManager.GetEntitiesWithComponent(typeof(T));
        }

    }
}

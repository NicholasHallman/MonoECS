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

        public World()
        {
            entities = new List<Entity>();
            componentManager = new ComponentManager();
            services = new Dictionary<string, List<Service>>();
            services["default"] = new List<Service>();
        }

        public Entity CreateEntity()
        {
            Entity entity = new Entity();
            entities.Add(entity);
            return entity;
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

        public World RemoveSystem(Service service, string group)
        {
            services[group].Remove(service);
            return this;
        }

        public void Run(string group = "default")
        {
            foreach( Service service in services[group])
            {
                service.Run();
            }
        }

    }
}

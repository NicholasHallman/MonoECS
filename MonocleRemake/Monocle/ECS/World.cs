using ICSharpCode.Decompiler.CSharp.Syntax;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonocleRemake.Monocle.ECS;
using ECS.Monocle;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Humanizer;

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

        private static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }

        public void LoadEntities(string path)
        {
            EntityLoader loader = new EntityLoader();
            dynamic[] potentialEntities = loader.Load(path);
            foreach(dynamic potentialEntity in potentialEntities)
            {
                Entity e = this.CreateEntity();
                foreach(dynamic component in potentialEntity)
                {
                    InitializeComponent(e, component);
                }
            }
            return;
        }

        private void SetField(Component component, dynamic property)
        {
            FieldInfo field = component.GetType().GetField(property.Key);
            if (field.FieldType == typeof(Texture2D))
            {
                field.SetValue(component, content.Load<Texture2D>(property.Value));
            }
            else if(field.FieldType == typeof(Vector2))
            {
                Vector2 vec = new Vector2();
                foreach(dynamic p in property.Value)
                {
                    if (p.Key == "X") vec.X = Convert.ChangeType(p.Value, typeof(float));
                    if (p.Key == "Y") vec.Y = Convert.ChangeType(p.Value, typeof(float));
                }
                field.SetValue(component, vec);
            }
            else
            {
                field.SetValue(component, Convert.ChangeType(property.Value, field.FieldType));
            }
        }

        private void InitializeComponent(Entity e, dynamic Component)
        {
            string compName = Component.Key;
            Type compType = GetType("ECS.Monocle."+compName);
            if (compType == null) throw new Exception("Component does note exist");
            typeof(Entity).GetMethod("AddComponent").MakeGenericMethod(compType).Invoke(e, new object[] { });
            var component = (Component)typeof(Entity).GetMethod("GetComponent").MakeGenericMethod(compType).Invoke(e, new object[] { });

            foreach (dynamic property in Component.Value)
            {
                SetField(component, property);
            }
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

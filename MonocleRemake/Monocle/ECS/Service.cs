using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Serialization;

namespace ECS
{
    abstract class Service
    {
        protected Query query;

        public Service AddQuery(Query query)
        {
            this.query = query;
            return this;
        }

        public void IgnoreCache()
        {
            this.query.useCache = false;
        }

        public abstract void Execute(Entity[] entities, World w);

        public void Run(World w)
        {
            Execute(query.Run(), w);
        }
    }
}

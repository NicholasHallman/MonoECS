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
        private Query query;

        public Service AddQuery(Query query)
        {
            this.query = query;
            return this;
        }

        public abstract void Execute(Entity[] entities);

        public void Run()
        {
            Execute(query.All());
        }
    }
}

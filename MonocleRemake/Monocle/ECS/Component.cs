using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace ECS
{
    class Component
    {
        protected Entity entity { get; set; }

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        public Entity GetEntity()
        {
            return entity;
        }
    }
}

    

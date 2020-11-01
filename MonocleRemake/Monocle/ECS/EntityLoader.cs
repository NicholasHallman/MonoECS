using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MonocleRemake.Monocle.ECS
{
    class EntityLoader
    {
        public EntityLoader() { }

        public dynamic[] Load(string path)
        {
            string pathName = Path.GetFullPath(path);
            try
            {
                string yamlEntity = System.IO.File.ReadAllText(pathName);

                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

                return deserializer.Deserialize<dynamic[]>(yamlEntity); ;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

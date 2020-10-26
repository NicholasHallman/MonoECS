using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ECS;

namespace Monocle.Tests
{

    class TestComponent : Component
    {
        public string tag { get; set; }
    }

    class TestService : Service
    {
        public override void Execute(Entity[] entities, World w)
        {
            foreach (Entity e in entities)
            {
                TestComponent c = e.GetComponent<TestComponent>();
                c.tag = "changed";
            }
        }
    }

    [TestClass]
    public class ECSTests
    {
        [TestMethod]
        public void MakeEntity()
        {
            World w = new World();
            Entity e = w.CreateEntity();
            Assert.IsTrue(e.id != null);
        }

        [TestMethod]
        public void MakeEntityWithComponents()
        {
            World w = new World();
            Entity e = w.CreateEntity()
                .AddComponent<TestComponent>();
            TestComponent c = e.GetComponent<TestComponent>();

            c.tag = "Player";

            TestComponent tag = e.GetComponent<TestComponent>();
            Assert.AreEqual(c, tag);
        }

        [TestMethod]
        public void ServiceModifiesEntityComponent()
        {
            World w = new World();
            Entity e = w.CreateEntity()
                .AddComponent<TestComponent>();

            Type[] testComponentA = new Type[] { typeof(TestComponent) };

            Service s = new TestService()
                .AddQuery(new Query(testComponentA));

            w.AddService(s);

            w.Run();

            Assert.IsTrue(e.GetComponent<TestComponent>().tag == "changed");
        }

    }
}

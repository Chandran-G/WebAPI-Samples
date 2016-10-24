using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAPI.Tests
{
    [SetUpFixture]
    public class AutofacContainer
    {
        public static IContainer Container { get; set; }

        [OneTimeSetUp]
        public void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ServiceTestModule());

            Container = builder.Build();
        }

        public static ILifetimeScope BeginLifetimeScope()
        {
            return Container.BeginLifetimeScope();
        }
    }
}

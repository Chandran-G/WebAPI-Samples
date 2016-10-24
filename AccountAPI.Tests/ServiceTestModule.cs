using Account.Repository.Contract;
using Account.ServiceLayer;
using Account.ServiceLayer.Contract;
using Autofac;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAPI.Tests
{
    public class ServiceTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(Substitute.For<IAccountRepository>()).As<IAccountRepository>();
            builder.RegisterType<AccountService>().As<IAccountService>();
        }
    }
}

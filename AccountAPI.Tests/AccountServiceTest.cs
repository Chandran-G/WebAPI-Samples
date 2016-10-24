using System;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using Account.DomainModel;
using Autofac;
using Account.Repository.Contract;
using System.Linq;
using Account.ServiceLayer.Contract;

namespace AccountAPI.Tests
{
    [TestFixture]
    public class AccountServiceTest
    {
        private IEnumerable<UserAccount> _sampleUserAccountData = null;
        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _sampleUserAccountData = new List<UserAccount>()
            {
                new UserAccount { Id=1, Name = "test Name", Address = "20 Glover Avenue", Email = "test@napower.com", Postal = "06850" },
                new UserAccount { Id=2,  Name = "test guy", Address = "20 Glover Avenue", Email = "test@napower.com", Postal = "06850" },
                new UserAccount { Id=3, Name = "Test john Doe", Address = "243 America Avenue", Email = "test@test.com", Postal = "07719" },
                new UserAccount { Id=4, Name = "Test Jane Doe", Address = "123 USA Avenue", Email = "test2@test.com", Postal = "06890" }
        };

        }

        [Test]
        public void CreateUserAccountTest_Pass()
        {
            using (ILifetimeScope testScope = AutofacContainer.BeginLifetimeScope())
            {
                int testUserId = 1;
                IAccountRepository accountRepository = testScope.Resolve<IAccountRepository>();
                accountRepository.Add(Arg.Any<UserAccount>()).Returns(_sampleUserAccountData.SingleOrDefault(a => a.Id == testUserId));
                UserAccount userAccount = testScope.Resolve<IAccountService>().Add( new UserAccount());
                Assert.IsNotNull(userAccount);
                Assert.AreEqual(userAccount.Id, testUserId);
            }
        }

        [Test]
        public void GetUserAccountTest_Pass()
        {
            using (ILifetimeScope testScope = AutofacContainer.BeginLifetimeScope())
            {
                int testUserId = 2;
                IAccountRepository accountRepository = testScope.Resolve<IAccountRepository>();
                accountRepository.GetUserAccount(Arg.Any<int>()).Returns(_sampleUserAccountData.SingleOrDefault( a=> a.Id == testUserId));

                UserAccount userAccount =testScope.Resolve<IAccountService>().GetUserAccount(testUserId);
                Assert.IsNotNull(userAccount);
                Assert.AreEqual(userAccount.Id, testUserId);
            }
        }

        [Test]
        public void GetUserAccountTest_InvalidId()
        {
            using (ILifetimeScope testScope = AutofacContainer.BeginLifetimeScope())
            {
                int testUserId = 10;
                IAccountRepository accountRepository = testScope.Resolve<IAccountRepository>();
                accountRepository.GetUserAccount(Arg.Any<int>()).Returns(_sampleUserAccountData.SingleOrDefault(a => a.Id == testUserId));

                UserAccount userAccount = testScope.Resolve<IAccountService>().GetUserAccount(testUserId);
                Assert.IsNull(userAccount);
            }
        }
    }


}

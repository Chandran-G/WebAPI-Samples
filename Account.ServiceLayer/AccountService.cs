using Account.ServiceLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.DomainModel;
using Account.Repository.Contract;

namespace Account.ServiceLayer
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public UserAccount Add(UserAccount item)
        {
            return _accountRepository.Add(item);
        }

        public IEnumerable<UserAccount> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public UserAccount GetUserAccount(int id)
        {
            return _accountRepository.GetUserAccount(id);
        }

        public void Remove(int id)
        {
            _accountRepository.Remove(id);
        }

        public bool Update(UserAccount item)
        {
            return _accountRepository.Update(item);
        }
    }
}

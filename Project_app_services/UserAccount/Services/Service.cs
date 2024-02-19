using System.Security.Principal;
using UserAccount.Exceptions;
using UserAccount.Model;
using UserAccount.Repository;

namespace UserAccount.Services
{
    public class Service : IService
    {
        private readonly IRepo repo;
        public Service(IRepo repo)
        {
            this.repo = repo;
        }

        public void AddAccount(AccountDetails accountdetails)
        {
            var ac = repo.GetAccountDetailsById(accountdetails.Mobile_No); 
            if (ac == null)
            {
                repo.AddAccount(accountdetails);
            }
            else
            {
                throw new AccountAlreadyExistsException($"Account with mobile no {accountdetails.Mobile_No} already exists");
            }
        }

        public void DeleteAccount(string Mobile_No)
        {
            var ac = GetAccountDetailsById(Mobile_No);
            if (ac == null)
            {
                throw new AccountNotFoundException($"Account with mobile no {Mobile_No} not found");
            }
            else
            {
                repo.DeleteAccount(Mobile_No);
            }
        }

        public List<AccountDetails> GetAccount()
        {
            return repo.GetAccount();
        }

        public AccountDetails GetAccountDetailsById(string Mobile_No)
        {
            var ac = repo.GetAccountDetailsById(Mobile_No);
            if (ac == null)
            {
                throw new AccountNotFoundException($"Account with mobile no {Mobile_No} not found");
            }
            else
            {
                return repo.GetAccountDetailsById(Mobile_No);
            }
        }

        public void UpdateAccount(string Mobile_No, AccountDetails accountdetails)
        {
            var ac = GetAccountDetailsById(Mobile_No);
            if (ac == null)
            {
                throw new AccountNotFoundException($"Account with mobile no {Mobile_No} not found");
            }
            else
            {
                repo.UpdateAccount(Mobile_No,accountdetails);
            }
        }
    }
}

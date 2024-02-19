using UserAccount.Model;

namespace UserAccount.Repository
{
    public interface IRepo
    {
        public List<AccountDetails> GetAccount();
        public AccountDetails GetAccountDetailsById(string Mobile_No);
        public void AddAccount(AccountDetails accountdetails);
        public void UpdateAccount(string Mobile_No, AccountDetails accountdetails);
        public void DeleteAccount(string Mobile_No);

    }
}

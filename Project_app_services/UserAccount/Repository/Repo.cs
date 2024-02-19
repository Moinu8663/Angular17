using UserAccount.Model;

namespace UserAccount.Repository
{
    public class Repo : IRepo
    {
        private readonly AccountDBcontext dbcontext;
        public Repo(AccountDBcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public void AddAccount(AccountDetails accountdetails)
        {
            dbcontext.AccDetails.Add(accountdetails);
            dbcontext.SaveChanges();
        }

        public void DeleteAccount(string Mobile_No)
        {
            var ac = GetAccountDetailsById(Mobile_No);
            dbcontext.AccDetails.Remove(ac);
            dbcontext.SaveChanges();
        }

        public List<AccountDetails> GetAccount()
        {
            return dbcontext.AccDetails.ToList();
        }

        public AccountDetails GetAccountDetailsById(string Mobile_No)
        {
            return dbcontext.AccDetails.Where(a => a.Mobile_No==Mobile_No).FirstOrDefault();
        }

        public void UpdateAccount(string Mobile_No, AccountDetails accountdetails)
        {
            var ac = GetAccountDetailsById(Mobile_No);
            ac.DOB = accountdetails.DOB;
            ac.Bio = accountdetails.Bio;
            ac.Address = accountdetails.Address;
            dbcontext.SaveChanges();
        }
    }
}

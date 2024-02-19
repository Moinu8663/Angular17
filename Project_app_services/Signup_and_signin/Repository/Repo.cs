using Signup_and_signin.Model;

namespace Signup_and_signin.Repository
{
    public class Repo : IRepo
    {
        private readonly UserDBcontext dbcontext1;

        public Repo(UserDBcontext dbcontext)
        {
            this.dbcontext1 = dbcontext;
        }

        public User LoginUser(User user)
        {
            var userobj = dbcontext1.Users.Where(u => u.Mobile_No == user.Mobile_No && u.Password == user.Password).FirstOrDefault();
            return userobj;
        }

        public void RegisterUser(User user)
        {
            dbcontext1.Users.Add(user);
            dbcontext1.SaveChanges();
        }

        public User GetUserByMobileNo(string Mobile_No)
        {
            return dbcontext1.Users.Where(u => u.Mobile_No == Mobile_No).FirstOrDefault();
           
        }

        public List<User> Getall()
        {
            return dbcontext1.Users.ToList();
        }

        public void UpdateUser(string Mobile_No, User user)
        {
            var us =GetUserByMobileNo(Mobile_No);
            us.Mobile_No =user.Mobile_No;
            us.Email = user.Email;
            us.First_name = user.First_name;
            us.Last_name = user.Last_name;
            us.Password = user.Password;
            us.Con_password = user.Con_password;
        }

        public void DeleteUser(string Mobile_No)
        {
            var us = GetUserByMobileNo(Mobile_No);
            dbcontext1.Users.Remove(us);
            dbcontext1.SaveChanges();
        }
    }
}

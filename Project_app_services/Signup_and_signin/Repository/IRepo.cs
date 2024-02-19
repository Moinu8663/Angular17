using Signup_and_signin.Model;
using System.Security.Principal;

namespace Signup_and_signin.Repository
{
    public interface IRepo
    {
        void RegisterUser(User user);
        public List<User> Getall();
        public User GetUserByMobileNo(string Mobile_No);
        public void UpdateUser(string Mobile_No, User user);
        public void DeleteUser(string Mobile_No);
        User LoginUser(User user);
    }
}

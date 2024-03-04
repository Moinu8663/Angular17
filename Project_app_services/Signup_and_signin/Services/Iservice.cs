using Signup_and_signin.Model;

namespace Signup_and_signin.Services
{
    public interface Iservice
    {
        void RegisterUser(User user);
        public List<User> Getall();
        User GetUserByMobileNo(string Mobile_No);
        public void UpdateUser(string Mobile_No, User user);
        public void DeleteUser(string Mobile_No);
        User LoginUser(User user);
    }
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}

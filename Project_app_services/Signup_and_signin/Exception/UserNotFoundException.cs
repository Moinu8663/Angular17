namespace Signup_and_signin.Exception
{
    public class UserNotFoundException:FormatException
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}

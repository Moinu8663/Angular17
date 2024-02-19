namespace Signup_and_signin.Exception
{
    public class UserAlreadyExistsException : FormatException
    {
        public UserAlreadyExistsException(string massage):base(massage) { }
    }
}

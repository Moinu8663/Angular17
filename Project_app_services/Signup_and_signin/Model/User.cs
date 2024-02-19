using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Signup_and_signin.Model
{
    public class User
    {
        [Key]
        public string Mobile_No { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Con_password { get; set; }
        public string? First_name { get; set; }
        public string? Last_name { get; set;}

    }
}

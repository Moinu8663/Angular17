using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAccount.Model
{
    public class AccountDetails
    {
        public string? First_name { get; set; }
        public string? Last_name { get; set; }
        [Key]
        public string? Mobile_No { get; set; }
        public string? Email { get; set; }
        public string? DOB {  get; set; }
        public string? Address { get; set;}
        public string? Bio { get; set; }
        public string? Role { get; set; }

    }
}

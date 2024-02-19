using System.ComponentModel.DataAnnotations;

namespace ContectUs.Model
{
    public class Contect
    {
        [Key]
        public string Mobile_No {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}

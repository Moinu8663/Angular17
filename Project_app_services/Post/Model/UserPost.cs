using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Model
{
    public class UserPost
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string? Mobile_No { get; set; }
        public string? Post { get; set;}

    }
}

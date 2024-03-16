using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Model
{
    public class UserPost
    {
        [Key]
        public int PostId { get; set; }
        public string? Mobile_No { get; set; }
        public string? Post { get; set;}

    }
}

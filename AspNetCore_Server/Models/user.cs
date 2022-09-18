using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetCore_Server.Models;

namespace AspNetCore_Server.Models
{
    public class user 
    {
        [Column("DateOfJoining")]
        public DateTime dateofjoining { get; set; } = DateTime.Now;
        [Column("Activity", TypeName = "nvarchar(150)")]
        public int activity { get; set; }
        public List<Article> articles { get; set; }
        [ForeignKey("UserId")]
        public Applicationuser userid { get; set; }
        [Column("Image", TypeName = "nvarchar(150)")]
        public string image { get; set; } = "~/Media";
        [Column("Bio", TypeName = "nvarchar(150)")]
        public string bio { get; set; }
        [Column("Lastname", TypeName = "nvarchar(150)")]
        public string LastName { get; set; }
        [Column("Firstname", TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }
    }
}

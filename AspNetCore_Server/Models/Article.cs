using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore_Server.Models
{
    public class Article
    {
        [Key]
        [Column("Id")]
        public int id { get; set; }
        [Required]
        [Column("Title")]
        [DataType(DataType.Text)]
        public string title { get; set; }
        [Required]
        [Column("Description")]
        [DataType(DataType.MultilineText)]
        public string text { get; set; }
        [Column("ContentUrl")]
        [DataType(DataType.Text)]
        public string? content { get; set; }
        [Column("Time")]
        [DataType(DataType.DateTime)]
        public DateTime publish_ime{ get; set; } = DateTime.Now;
        [Column("Likes")]
        public int likes { get; set; } = 0;
        [Column("dislike")]
        public int dislikes { get; set; } = 0;
        [ForeignKey("UserId")]
        public Applicationuser writer{ get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required, StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(300000)]
        public string Content { get; set; } = string.Empty;

        [Required, DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public User? Author { get; set; }
    }
}
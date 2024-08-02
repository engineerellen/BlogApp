using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(255)]
        public string Username { get; set; } = "";

        [Required, StringLength(255)]
        public string PasswordHash { get; set; }= "";

        [Required, StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";

        public ICollection<Post> Posts { get; set; }
    }
}
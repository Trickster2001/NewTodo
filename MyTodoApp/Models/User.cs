using System.ComponentModel.DataAnnotations;

namespace MyTodoApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Password { get; set; }

        public List<Todo>? Todos { get; set; }
    }
}

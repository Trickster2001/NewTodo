using System.ComponentModel.DataAnnotations;

namespace MyTodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string? Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}

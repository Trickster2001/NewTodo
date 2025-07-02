using System.ComponentModel.DataAnnotations;

namespace MyTodoApp.Models
{
    public enum TodoStatus { 
    NotStarted,
    InProgress,
    Completed
    }

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
        public TodoStatus Status { get; set; } = TodoStatus.NotStarted;

        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}

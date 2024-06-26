using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskmateAPI.Model
{
    // Dependent entity, discovered by convention
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }

        // Specify Foreign Key to User 
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation property to principal
        public User User { get; set; } = null!;
    }
}

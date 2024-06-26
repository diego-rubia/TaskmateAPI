using System.ComponentModel.DataAnnotations;

namespace TaskmateAPI.Model
{
    // Parent Entity, Discovered by convention
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Department DepartmentName { get; set; }

        // Collection navigation property for dependents, is an ICollection
        // since One user can have many Tasks
        public ICollection<Task> Tasks { get; } = new List<Task>();
    }
}

using Bogus;
using TaskmateAPI.Model;

namespace TaskmateAPI.Data
{
    public class TaskDataGenerator
    {
        // Model.Task was used instead of just Task to clear ambiguity 
        Faker<Model.Task> taskDataGenerator;

        public TaskDataGenerator(int uid)
        {
            const int seed = 123;
            Randomizer.Seed = new Random(seed);
            // UserId is the Foreign Key which is determined by the uid passed in this constructor
            taskDataGenerator = new Faker<Model.Task>()
                .RuleFor(t => t.TaskName, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Text())
                .RuleFor(t => t.Priority, f => f.PickRandom<TaskPriority>())
                .RuleFor(t => t.UserId, uid);
        }

        public List<Model.Task> GenerateTasks(int amount)
        {
            return taskDataGenerator.Generate(amount);
        }
    }
}

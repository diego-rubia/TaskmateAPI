using TaskmateAPI.Model;
using Bogus;

namespace TaskmateAPI.Data
{
    public class UserDataGenerator
    {
        Faker<User> userDataGenerator;

        public UserDataGenerator()
        {
            const int seed = 123;
            Randomizer.Seed = new Random(seed);

            userDataGenerator = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.DepartmentName, f => f.PickRandom<Department>());
        }

        public List<User> GenerateUsers(int amount)
        {
            return userDataGenerator.Generate(amount);
        }
    }
}

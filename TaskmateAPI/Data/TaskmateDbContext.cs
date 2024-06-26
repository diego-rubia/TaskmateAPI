using Microsoft.EntityFrameworkCore;
using TaskmateAPI.Model;


namespace TaskmateAPI.Data
{
    public class TaskmateDbContext : DbContext
    {
        // This is here to prevent errors in the migration/seeding process, just in case
        public TaskmateDbContext()
        {
            
        }

        public TaskmateDbContext(DbContextOptions<TaskmateDbContext> options) 
            : base(options)
        {

        }

        // DbSets
        public DbSet<User>  Users { get; set; }
        public DbSet<Model.Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Build the database based on appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            // Get the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

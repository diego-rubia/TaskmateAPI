using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskmateAPI.Model;

namespace TaskmateAPI.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly TaskmateDbContext _dbContext;

        public DataRepository(TaskmateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET endpoint handler functions
        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public List<Model.Task> GetAllTasks()
        {
            return _dbContext.Tasks.ToList();
        }

        public User GetUserById(int uid)
        {
            return _dbContext.Users.Where(x => x.UserID == uid).FirstOrDefault();
        }

        public Model.Task GetTaskById(int tid)
        {
            return _dbContext.Tasks.Where(x => x.TaskId == tid).FirstOrDefault();
        }

        public List<Model.Task> GetAllTasksByUserId(int uid)
        {
            return _dbContext.Tasks.Where(x => x.UserId == uid).ToList();
        }

        // PUT (update) endpoint handler functions
        public User UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return _dbContext.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
        }

        public Model.Task UpdateTask(Model.Task task)
        {
            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();
            return _dbContext.Tasks.Where(x => x.TaskId == task.TaskId).FirstOrDefault();
        }

        // POST (create) endpoint handler functions
        public User AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return _dbContext.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
        }

        public Model.Task AddTask(Model.Task task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
            return _dbContext.Tasks.Where(x => x.TaskId == task.TaskId).FirstOrDefault();
        }

        // DELETE endpoints
        public int DeleteUser(int uid)
        {
            // Be careful with this since deleting a user will delete all tasks assigned to the user
            // Note that ExecuteDelete() returns an int
            var deleteUser = _dbContext.Users.Where(x => x.UserID == uid).ExecuteDelete();
            _dbContext.SaveChanges();
            return deleteUser;
        }

        public int DeleteTask(int tid)
        {
            var deleteTask = _dbContext.Tasks.Where(x => x.TaskId == tid).ExecuteDelete();
            _dbContext.SaveChanges();
            return deleteTask;
        }
    }
}

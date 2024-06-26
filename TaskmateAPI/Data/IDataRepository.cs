using TaskmateAPI.Model;

namespace TaskmateAPI.Data
{
    public interface IDataRepository
    {
        List<Model.Task> GetAllTasks();
        List<User> GetAllUsers();
        Model.Task GetTaskById(int tid);
        User GetUserById(int uid);
        List<Model.Task> GetAllTasksByUserId(int uid);
        User UpdateUser(User user);
        Model.Task UpdateTask(Model.Task task);
        User AddUser(User user);
        Model.Task AddTask(Model.Task task);
        int DeleteUser(int uid);
        int DeleteTask(int tid);
    }
}
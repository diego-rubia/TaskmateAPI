using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskmateAPI.Data;
using TaskmateAPI.Model;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDb");

// Dependency injections, Add the service for the Database
builder.Services.AddDbContext<TaskmateDbContext>(x => x.UseSqlServer(connectionString));

// Dependency injection for the IDataRepository and DataRepository which contains the definition of the API routes
builder.Services.AddScoped<IDataRepository, DataRepository>();

// Add DatabaseSeeder service which will seed the data using Bogus
builder.Services.AddTransient<DatabaseSeeder>();

// Add Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI
app.UseSwaggerUI();

// Function that will start database seeding for the Database Tables: dotnet run seeddata in the terminal
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    SeedDatabaseTables(app);
}

void SeedDatabaseTables(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DatabaseSeeder>();
        // Seed the users table then seed the tasks table
        service.SeedUsers();
        service.SeedTasks();
    }
}

app.UseSwagger(s => s.SerializeAsV2 = true);

// API endpoints, GET requests
app.MapGet("/", () => "Hello. Please refer to the README file on how to use the API");

app.MapGet("/users", ([FromServices] IDataRepository db) => db.GetAllUsers());

app.MapGet("/users/{uid}", ([FromServices] IDataRepository db, int uid) => db.GetUserById(uid));

app.MapGet("/users/{uid}/tasks", ([FromServices] IDataRepository db, int uid) => db.GetAllTasksByUserId(uid));

app.MapGet("/tasks", ([FromServices] IDataRepository db) => db.GetAllTasks());

app.MapGet("/tasks/{tid}", ([FromServices] IDataRepository db, int tid) => db.GetTaskById(tid));

// PUT endpoints for updating Tasks and User data
app.MapPut("/users", ([FromServices] IDataRepository db, User user) => db.UpdateUser(user));

app.MapPut("/tasks", ([FromServices] IDataRepository db, TaskmateAPI.Model.Task task) => db.UpdateTask(task));

// POST endpoints for adding Tasks and User Data
app.MapPost("/users", ([FromServices] IDataRepository db, User user) => db.AddUser(user));

app.MapPost("/tasks", ([FromServices] IDataRepository db, TaskmateAPI.Model.Task task) => db.AddTask(task));

// DELETE endpoints
app.MapDelete("/users/{uid}", ([FromServices] IDataRepository db, int uid) => db.DeleteUser(uid));

app.MapDelete("/tasks/{tid}", ([FromServices] IDataRepository db, int tid) => db.DeleteTask(tid));

app.Run();

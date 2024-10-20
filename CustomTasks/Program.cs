using CustomTasks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//
// USER
//

// Insert Users
app.MapPost("/user/create", async ([FromBody] CustomTasks.Models.User user, [FromServices] AppDataContext context) => 
{
    var existsEmail = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
    if (existsEmail!= null)
    {
        return Results.Conflict("The email is already in use!");
    }

    context.Users.Add(user);
    context.SaveChanges();
    return Results.Created("", user);
});

// To Check User
app.MapGet("/user/{id}", async (int id, [FromServices] AppDataContext context) =>
{
    var user = await context.Users.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("User not found!");
    }
    return Results.Ok(user);
});

// List User
app.MapGet("/user/list", (AppDataContext context) =>
{
    var usersList =  context.Users.ToList();
    if (usersList.Count == 0) {
        return Results.NotFound("404 - No users found.");
    }
    return Results.Ok(usersList);
});


// Update Users
app.MapPut("/user/update/{id}", async (int id, CustomTasks.Models.User userInput, [FromServices] AppDataContext context) =>
{
    var user = await context.Users.FindAsync(id);

    if (user == null)
    {
        return Results.NotFound("404 - The ID does not match any User!");
    }

    user.Username = userInput.Username;
    user.Email = userInput.Email;
    user.Password = userInput.Password;

    await context.SaveChangesAsync();
    return Results.Ok("User information has been updated!");
});

// Delete Users
app.MapDelete("/user/delete/{id}", async (int id,  [FromServices] AppDataContext context) => 
{
     var user = await context.Users.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("404 - The ID does not match any User!");
    }

    context.Users.Remove(user);
    await context.SaveChangesAsync();
    return Results.Ok("User removed successfully!");
});

//
// TASKS
//

// Insert Task
app.MapPost("/tasks/create", ([FromBody] CustomTasks.Models.Task task, [FromServices] AppDataContext context) => 
{
    context.Tasks.Add(task);
    context.SaveChanges();
    return Results.Created("", task);
});

// List Task
app.MapGet("/tasks/list", (AppDataContext context) =>
{
    var tasksList =  context.Tasks.ToList();
    if (tasksList.Count == 0)
    {
        return Results.NotFound("404 - No tasks found.");
    }
    return Results.Ok(tasksList);
});

// Update Task
app.MapPut("/tasks/update/{id}", async (int id, CustomTasks.Models.Task task, [FromServices] AppDataContext context) =>
{
    var tarefa = await context.Tasks.FindAsync(id);

    if (tarefa == null){
        return Results.NotFound("404 - The ID does not match any Task!");
    }

    tarefa.Name = task.Name;
    tarefa.Description = task.Description;
    tarefa.IsCompleted = task.IsCompleted;
    tarefa.CreatedAt = task.CreatedAt;
    tarefa.Labels = task.Labels;
    
    await context.SaveChangesAsync();
    return Results.Ok("Task information has been updated!");
});

// Remove Task
app.MapDelete("/tasks/delete/{id}", async (int id,  [FromServices] AppDataContext context) => 
{
     var produto = await context.Tasks.FindAsync(id);
    if (produto == null)
    {
        return Results.NotFound("404 - The ID does not match any Task!");
    }

    context.Tasks.Remove(produto);
    await context.SaveChangesAsync();
    return Results.Ok("Task removed successfully!");
});

//
//  LABEL
//

// Insert Label
app.MapPut("/label/create", async ([FromBody] CustomTasks.Models.Label label, [FromServices] AppDataContext context) =>
{
    context.Labels.Add(label);
    await context.SaveChangesAsync();
    return Results.Created("", label);
});

// Delete Label
app.MapDelete("/label/delete/{id}", async (int id,  [FromServices] AppDataContext context) => 
{
     var label = await context.Labels.FindAsync(id);
    if (label == null)
    {
        return Results.NotFound("404 - ID does not match any Label!");
    }

    context.Labels.Remove(label);
    await context.SaveChangesAsync();
    return Results.Ok("Label removed successfully!");
});

app.MapGet("/labels/list/{userId}", async (int userId, AppDataContext context) =>
{
    var label = await context.Labels.Where(l => l.UserId == userId)
    .ToListAsync();
    if (label == null)
    {
        return Results.NotFound("No labels found for this user!");
    }

    return Results.Ok(label);
});




app.Run();

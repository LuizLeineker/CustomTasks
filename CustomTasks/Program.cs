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
app.MapPost("/user/create", ([FromBody] CustomTasks.Models.User user, [FromServices] AppDataContext context) => 
{
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
        return Results.NotFound("User not found !");
    }
    return Results.Ok(user);
});

// Update Users
app.MapPut("/user/update/{id}", async (int id, CustomTasks.Models.User userInput, [FromServices] AppDataContext context) =>
{
    var user = await context.Users.FindAsync(id);

    if (user == null)
    {
        return Results.NotFound("404 - The ID does not match any User !");
    }

    user.Username = userInput.Username;
    user.Email = userInput.Email;
    user.Password = userInput.Password;

    await context.SaveChangesAsync();
    return Results.Ok("User information has been updated !");
});

// Delete Users
app.MapDelete("/user/delete/{id}", async (int id,  [FromServices] AppDataContext context) => 
{
     var user = await context.Users.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("404 - The ID does not match any User !");
    }

    context.Users.Remove(user);
    await context.SaveChangesAsync();

    return Results.Ok("User removed successfully !");
});

//
// TASKS
//

// Criar Tarefas
app.MapPost("/tasks/create", ([FromBody] CustomTasks.Models.Task task, [FromServices] AppDataContext context) => 
{
    context.Tasks.Add(task);
    context.SaveChanges();
    return Results.Created("", task);
});

// Listar Tarefas
app.MapGet("/tasks/list", (AppDataContext context) =>
{
    var tarefas =  context.Tasks.ToList();
    return Results.Ok(tarefas);
});

// Update Tarefas
app.MapPut("/tasks/update/{id}", async (int id, CustomTasks.Models.Task task, [FromServices] AppDataContext context) =>
{
    var tarefa = await context.Tasks.FindAsync(id);

    if (tarefa == null){
        return Results.NotFound("404 - The ID does not match any Task !");
    }

    tarefa.Name = task.Name;
    tarefa.Description = task.Description;
    tarefa.IsCompleted = task.IsCompleted;
    tarefa.CreatedAt = task.CreatedAt;
    tarefa.Labels = task.Labels;
    
    await context.SaveChangesAsync();
    return Results.Ok("Task information has been updated!");
});

// Remover Tarefas

app.MapDelete("/tasks/delete/{id}", async (int id,  [FromServices] AppDataContext context) => 
{
     var produto = await context.Tasks.FindAsync(id);
    if (produto == null)
    {
        return Results.NotFound("404 - The ID does not match any Task !");
    }

    context.Tasks.Remove(produto);
    await context.SaveChangesAsync();

    return Results.Ok("Task removed successfully !");
});





app.Run();

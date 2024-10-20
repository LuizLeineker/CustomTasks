using CustomTasks.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//
// USER
//

// Insert Users
app.MapPost("/user/create", ([FromBody] User user, [FromServices] AppDataContext context) => 
{
    context.Users.Add(user);
    context.SaveChanges();
    return Results.Created("", user);
});

// To Check User
app.MapGet("/user/{id}", async ([FromRoute] int id, [FromServices] AppDataContext context) =>
{
    var user = await context.Users.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("User not found!");
    }
    return Results.Ok(user);
});

// Update Users
app.MapPut("/user/update/{id}", async ([FromRoute] int id, [FromBody] User userInput, [FromServices] AppDataContext context) =>
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
app.MapDelete("/user/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
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

// Criar uma tarefa
app.MapPost("/tasks/create", ([FromBody] CustomTasks.Models.Task task, [FromServices] AppDataContext context) => 
{
    context.Tasks.Add(task);
    context.SaveChanges();
    return Results.Created("", task);
});

// Lista todas as tarefas existentes no banco
app.MapGet("/tasks/list", (AppDataContext context) =>
{
    var tarefas =  context.Tasks.ToList();
    return Results.Ok(tarefas);
});

// Atualiza os campos de nome, descrição, status e etiquetas relacionadas de uma tarefa em específico
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

// Remove uma tarefa em específico
app.MapDelete("/tasks/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
{
    var task = await context.Tasks.FindAsync(id);
    if (task == null)
    {
        return Results.NotFound("404 - The ID does not match any Task!");
    }

    context.Tasks.Remove(task);
    await context.SaveChangesAsync();

    return Results.Ok("Task removed successfully!");
});

app.Run();

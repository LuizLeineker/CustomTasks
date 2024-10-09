using CustomTasks.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Endpoint para a funcionalidade de criação e inserção de tarefas
app.MapPost("/tasks/create", ([FromBody] CustomTasks.Models.Task task, [FromServices] AppDataContext context) => 
{
    context.Tasks.Add(task);
    context.SaveChanges();
    return Results.Created("", task);
});

app.Run();

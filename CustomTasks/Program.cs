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
app.MapPost("/user/create", ([FromBody] CustomTasks.Models.User user, [FromServices] AppDataContext context) => 
{
    context.Users.Add(user);
    context.SaveChanges();
    return Results.Created("", user);
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

    if(tarefa == null){
        return Results.NotFound("404 - O ID não corresponde a nenhuma Tarefa!");
    }

    tarefa.Name = task.Name;
    tarefa.Description = task.Description;
    tarefa.IsCompleted = task.IsCompleted;
    tarefa.CreatedAt = task.CreatedAt;
    tarefa.Labels = task.Labels;
    
    await context.SaveChangesAsync();
    return Results.Ok("informações da Tarefa foram atualizadas! ");

});

// Remover Tarefas

app.MapDelete("/tasks/delete/{id}", async (int id,  [FromServices] AppDataContext context) => 
{
     var produto = await context.Tasks.FindAsync(id);
    if (produto == null)
    {
        return Results.NotFound("404 - O ID não corresponde a nenhuma Tarefa!");
    }

    context.Tasks.Remove(produto);
    await context.SaveChangesAsync();

    return Results.Ok("Tarefa removida com sucesso!");
});





app.Run();

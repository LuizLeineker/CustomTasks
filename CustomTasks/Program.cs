using CustomTasks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

// Habilitando CORS e política que permite requisições com origem (composta por esquema, domínio e porta) do website feito em React
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactWebsite", 
        configs => configs.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
});

var app = builder.Build();

app.MapGet("/", () => "Welcome to the CustomTasks API!");

//
// Usuários (Users)
//

// Cria usuário (caso o nome e email que serão usados já não existam)
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

// Vereficar o login pelo Nome, Email e Senha do Usuário
app.MapPost("/user/login", async ([FromBody] User LoginUser, [FromServices] AppDataContext context) =>
{
    var user = await context.Users
        .Where(u => u.Username == LoginUser.Username && u.Email == LoginUser.Email && u.Password == LoginUser.Password)
        .FirstOrDefaultAsync();
    if (user == null)
    {
        return Results.BadRequest("Name, email or password are incorrect.");
    }
    return Results.Ok("Login successfully!");
});


// Retorna os dados do usuário cujo id bata com o parâmetro passado através da URL (caso o id seja válido) 
app.MapGet("/user/{id}", async ([FromRoute] int id, [FromServices] AppDataContext context) =>
{
    var user = await context.Users.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("User not found!");
    }
    return Results.Ok(user);
});

// Lista todos os usuários cadastrados (caso exista algum)
app.MapGet("/user/list", ([FromServices] AppDataContext context) =>
{
    var usersList =  context.Users.ToList();
    if (usersList.Count == 0) {
        return Results.NotFound("404 - No users found.");
    }
    return Results.Ok(usersList);
});

// Atualiza as informações do usuário cujo id bata com o do parâmetro passado através da URL (caso o id seja válido)
app.MapPut("/user/update/{id}", async ([FromRoute] int id, [FromBody] User userChanges, [FromServices] AppDataContext context) =>
{
    var user = await context.Users.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("404 - The ID does not match any user!");
    }

    user.Username = userChanges.Username;
    user.Email = userChanges.Email;
    user.Password = userChanges.Password;

    await context.SaveChangesAsync();
    return Results.Ok("User information has been updated!");
});

// Remove o usuário cujo id bata com o do parâmetro passado através da URL (caso o id seja válido)
app.MapDelete("/user/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
{
    var user = await context.Users.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("404 - The ID does not match any user!");
    }

    context.Users.Remove(user);
    await context.SaveChangesAsync();
    return Results.Ok("User removed successfully!");
});

//
// Tarefas (Tasks)
//

// Cria uma tarefa com base no objeto do tipo task passado via parâmetro através do corpo da requisição
app.MapPost("/tasks/create", ([FromBody] CustomTasks.Models.Task task, [FromServices] AppDataContext context) => 
{
    context.Tasks.Add(task);
    context.SaveChanges();
    return Results.Created("", task);
});

// Lista todas as tarefas pertencentes ao usuário cujo id bata com o do parâmetro passado através da URL (caso o id corresponda a algum usuário efetivamente)
app.MapGet("/tasks/list/{userId}", ([FromRoute] int userId, [FromServices] AppDataContext context) =>
{
    User? user = context.Users.Include(u => u.Tasks).FirstOrDefault(u => u.UserId == userId);
    if (user == null) {
        return Results.NotFound("404 - The ID does not match any user!");
    }

    var userTasks = user.Tasks;
    if (userTasks == null || userTasks.Count() == 0) {
        return Results.NoContent();
    }

    return Results.Ok(userTasks.ToList());
});

// Atualiza as informações da tarefa com o valor do id passado via parâmetro através da URL
app.MapPut("/tasks/update/{id}", async ([FromRoute] int id, CustomTasks.Models.Task task, [FromServices] AppDataContext context) =>
{
    var tarefa = await context.Tasks.FindAsync(id);
    if (tarefa == null){
        return Results.NotFound("404 - The ID does not match any task!");
    }

    tarefa.Name = task.Name;
    tarefa.Description = task.Description;
    tarefa.IsCompleted = task.IsCompleted;
    tarefa.CreatedAt = task.CreatedAt;
    tarefa.Labels = task.Labels;
    
    await context.SaveChangesAsync();
    return Results.Ok("Task information has been updated!");
});

// Remove a tarefa com id correspondnete ao passado por parâmetro através da URL (caso o id de fato corresponda a alguma tarefa)
app.MapDelete("/tasks/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
{
    var produto = await context.Tasks.FindAsync(id);
    if (produto == null)
    {
        return Results.NotFound("404 - The ID does not match any task!");
    }

    context.Tasks.Remove(produto);
    await context.SaveChangesAsync();
    return Results.Ok("Task removed successfully!");
});

//
//  Etiquetas/Rótulos (Labels)
//

// Cria uma etiqueta/rótulo tomando como referência o objeto de rótulo passado por argumento através do corpo da requisição
app.MapPut("/label/create", async ([FromBody] Label label, [FromServices] AppDataContext context) =>
{
    context.Labels.Add(label);
    await context.SaveChangesAsync();
    return Results.Created("", label);
});

// Retorna todos os rótulos associados ao usuário com o id passado por argumento através da URL (caso ele possua algum e o id seja válido)
app.MapGet("/label/list/{userId}", ([FromRoute] int userId, [FromServices] AppDataContext context) =>
{
    User? user = context.Users.Include(u => u.Labels).FirstOrDefault(u => u.UserId == userId);
    if (user == null) {
        return Results.NotFound("404 - The ID does not match any user!");
    }

    var userLabels = user.Labels;
    if (userLabels == null || userLabels.Count() == 0) {
        return Results.NoContent();
    }

    return Results.Ok(userLabels.ToList());
});

// Remove o rótulo cujo id bata com o do parâmetro passado através da URL (caso o id de fato se refira a alguma etiqueta/rótulo existente no banco)
app.MapDelete("/label/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
{
    var label = await context.Labels.FindAsync(id);
    if (label == null)
    {
        return Results.NotFound("404 - ID does not match any label!");
    }

    context.Labels.Remove(label);
    await context.SaveChangesAsync();
    return Results.Ok("Label removed successfully!");
});

// Aplicando a política de CORS que permite requisições do wbesite em REACT (AllowReactWebsite)
app.UseCors("AllowReactWebsite");

app.Run();
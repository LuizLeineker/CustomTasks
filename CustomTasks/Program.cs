using CustomTasks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

// Habilitando CORS e política que permite requisições com origem (composta por esquema, domínio e porta) do website feito em React
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactWebsite", 
        configs => configs.WithOrigins("http://localhost:3000")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
});

var app = builder.Build();

app.MapGet("/", () => "Welcome to the CustomTasks API!");

//
// Usuários (Users)
//

// Cria usuário (caso o nome e email que serão usados já não existam)
app.MapPost("/user/create", async ([FromBody] User user, [FromServices] AppDataContext context) => 
{
    bool userExists = await context.Users.AnyAsync(u => u.Username.Equals(user.Username) || u.Email.Equals(user.Email));
    if (userExists)
    {
        return Results.Conflict("This username and/or email are already in use!");
    }

    // Aplicando a função de hashing à senha do usuário por motivos de segurança
    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

    context.Users.Add(user);
    await context.SaveChangesAsync();

    return Results.Created("", user);
});

// Verificar o login pelo Nome, Email e Senha do Usuário
app.MapPost("/user/login", async ([FromBody] User LoginUser, [FromServices] AppDataContext context) =>
{
    // Retorna o usuário com o email e a senha passadas via parâmetro
    var user = await context.Users.FirstOrDefaultAsync(u => u.Username.Equals(LoginUser.Username) && u.Email == LoginUser.Email);
    if (user is null)
    {
        return Results.BadRequest("Name or email are incorrect.");
    }

    bool passwordIsCorrect = BCrypt.Net.BCrypt.Verify(LoginUser.Password, user.Password);
    if (!passwordIsCorrect) 
    {
        return Results.BadRequest("The given password is incorrect.");
    }

    return Results.Ok("Login successfully!");
});

// Retorna os dados do usuário cujo id bata com o parâmetro passado através da URL (caso o id seja válido) 
app.MapGet("/user/{id}", ([FromRoute] int id, [FromServices] AppDataContext context) =>
{
    var user = context.Users
    .Include(u => u.Tasks)        // Incluir tarefas do usuário
    .ThenInclude(t => t.Labels)   // Incluir rótulos de cada tarefa
    .Include(u => u.Labels)       // Incluir os próprios rótulos do usuário
    .FirstOrDefault(u => u.UserId == id);
    if (user == null)
    {
        return Results.NotFound("User not found!");
    }
    return Results.Ok(user);
});

// Atualiza as informações do usuário cujo id bata com o do parâmetro passado através da URL (caso o id seja válido)
app.MapPut("/user/update/{id}", async ([FromRoute] int id, [FromBody] User userChanges, [FromServices] AppDataContext context) =>
{
    var user = await context.Users.FindAsync(id);
    if (user is null)
    {
        return Results.NotFound("404 - The ID does not match any user!");
    }

    user.Username = userChanges.Username;
    user.Email = userChanges.Email;
    user.Password = BCrypt.Net.BCrypt.HashPassword(userChanges.Password); // Aplicando hashing à nova senha do usuário

    await context.SaveChangesAsync();

    return Results.Ok("User information has been updated!");
});

// Remove o usuário cujo id bata com o do parâmetro passado através da URL (caso o id seja válido)
app.MapDelete("/user/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
{
    var user = await context.Users.FindAsync(id);
    if (user is null)
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
app.MapPost("/tasks/create", async ([FromBody] CustomTasks.Models.Task task, [FromServices] AppDataContext context) => 
{
    context.Tasks.Add(task);
    await context.SaveChangesAsync();

    return Results.Created("", task);
});

// Lista todas as tarefas pertencentes ao usuário cujo id bata com o do parâmetro passado através da URL (caso o id corresponda a algum usuário efetivamente)
app.MapGet("/tasks/list/{userId}", ([FromRoute] int userId, [FromServices] AppDataContext context) =>
{
    var task = context.Tasks.Include(t => t.Labels);
    User? user = context.Users
    .Include(u => u.Tasks)                      // Carrega as tarefas do usuário
    .ThenInclude(t => t.Labels)              // Carrega os rótulos associados a cada tarefa
    .FirstOrDefault(u => u.UserId == userId);  
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
    var matchingTask = await context.Tasks.FindAsync(id);
    if (matchingTask is null){
        return Results.NotFound("404 - The ID does not match any task!");
    }

    // Atualizando informações da tarefa
    matchingTask.Name = task.Name;
    matchingTask.Description = task.Description;
    matchingTask.IsCompleted = task.IsCompleted;
    matchingTask.Labels = task.Labels;
    
    await context.SaveChangesAsync();

    return Results.Ok("Task information has been updated!");
});

// Remove a tarefa com id correspondnete ao passado por parâmetro através da URL (caso o id de fato corresponda a alguma tarefa)
app.MapDelete("/tasks/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
{
    var matchingTask = await context.Tasks.FindAsync(id);
    if (matchingTask is null)
    {
        return Results.NotFound("404 - The ID does not match any task!");
    }

    context.Tasks.Remove(matchingTask);
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
    var matchingLabel = await context.Labels.FindAsync(id);
    if (matchingLabel is null)
    {
        return Results.NotFound("404 - ID does not match any label!");
    }

    context.Labels.Remove(matchingLabel);
    await context.SaveChangesAsync();
    
    return Results.Ok("Label removed successfully!");
});

// Aplicando a política de CORS que permite requisições do wbesite em REACT (AllowReactWebsite)
app.UseCors("AllowReactWebsite");

app.Run();
using CustomTasks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

// Habilitando CORS e política que permite requisições com origem (composta por esquema, domínio e porta) do website feito em React
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowReactWebsite", configs => 
                    configs.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
});

var app = builder.Build();

app.MapGet("/", () => "Welcome to the CustomTasks API!");

//
// Usuários (Users)
//

// Cria usuário (caso o nome e email que serão usados já não existam)
app.MapPost("/user/create", async ([FromBody] User inputUser, [FromServices] AppDataContext context) => 
{
    try 
    {
        // Aplicando a função de hashing à senha do usuário por motivos de segurança
        inputUser.Password = BCrypt.Net.BCrypt.HashPassword(inputUser.Password);

        await context.Users.AddAsync(inputUser);
        await context.SaveChangesAsync();       
    }
    catch (DbUpdateException)
    {
        return Results.Conflict("Username and/or email are already in use!");
    }

    return Results.Created("", inputUser);
});

// Verificar o login pelo Nome, Email e Senha do Usuário
app.MapPost("/user/login", async ([FromBody] User inputUser, [FromServices] AppDataContext context) =>
{
    // Retorna o usuário com o email e a senha passadas via parâmetro
    var user = await context.Users
    .FirstOrDefaultAsync(u => u.Username.Equals(inputUser.Username) && u.Email.Equals(inputUser.Email));
    if (user is null)
    {
        return Results.BadRequest("Name and/or email are incorrect.");
    }

    bool passwordIsCorrect = BCrypt.Net.BCrypt.Verify(inputUser.Password, user.Password);
    if (!passwordIsCorrect) 
    {
        return Results.Unauthorized();
    }

    return Results.Ok(user.Username);
});

// Retorna os dados do usuário cujo id bata com o parâmetro passado através da URL (caso o id seja válido) 
app.MapGet("/user/{username}", ([FromRoute] string username, [FromServices] AppDataContext context) =>
{
    // Obtendo lista com os rótulos padrão
    var defaultLabels = context.Labels.Where(u => u.UserId == null);

    var user = context.Users
    .Where(u => u.Username.Equals(username))
    .Include(u => u.Tasks)        // Inclui as tarefas do usuário em particular
    .ThenInclude(t => t.Labels)   // Inclui os rótulos de cada tarefa
    .Include(u => u.Labels)       // Inclui os rótulos do usuário em questão
    .FirstOrDefault();
    if (user is null)
    {
        return Results.NotFound("User not found.");
    }

    // Montando a lista de todas as labels do usuário (incluindo as padrões)
    user.Labels.AddRange(defaultLabels);
    
    return Results.Ok(user);
});

// Atualiza as informações do usuário cujo nome bata com o do parâmetro passado através da URL (caso o id seja válido)
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
    // Anexando cada uma das etiquetas presentes na tarefa ao contexto e marcando-as como não modificadas
    context.Labels.AttachRange(task.Labels);

    await context.Tasks.AddAsync(task);
    await context.SaveChangesAsync();

    return Results.Created("", task);
});

// Lista todas as tarefas pertencentes ao usuário cujo id bata com o do parâmetro passado através da URL (caso o id corresponda a algum usuário efetivamente)
app.MapGet("/tasks/list/{username}", ([FromRoute] string username, [FromServices] AppDataContext context) =>
{
    User? user = context.Users
    .Where(u => u.Username.Equals(username))     // Busca o usuário baseado no nome
    .Include(u => u.Tasks)                      // Carrega as tarefas do usuário
    .ThenInclude(t => t.Labels)                // Carrega os rótulos associados a cada tarefa
    .FirstOrDefault();  
    if (user is null) {
        return Results.NotFound("Given username does not match any user.");
    }

    return Results.Ok(user.Tasks.ToList());
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
app.MapGet("/label/list/{username}", ([FromRoute] int username, [FromServices] AppDataContext context) =>
{
    User? user = context.Users
    .Where(u => u.Username.Equals(username))
    .Include(u => u.Labels)
    .FirstOrDefault();
    if (user is null) {
        return Results.NotFound("Given username does not match any user.");
    }

    return Results.Ok(user.Labels.ToList());
});

// Remove o rótulo cujo id bata com o do parâmetro passado através da URL (caso o id de fato se refira a alguma etiqueta/rótulo existente no banco)
app.MapDelete("/label/delete/{id}", async ([FromRoute] int id,  [FromServices] AppDataContext context) => 
{
    var matchingLabel = await context.Labels.FindAsync(id);
    if (matchingLabel is null)
    {
        return Results.NotFound("Id does not match any label!");
    }

    context.Labels.Remove(matchingLabel);
    await context.SaveChangesAsync();
    
    return Results.Ok("Label removed successfully!");
});

// Aplicando a política de CORS que permite requisições do wbesite em REACT (AllowReactWebsite)
app.UseCors("AllowReactWebsite");

app.Run();
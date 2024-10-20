using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CustomTasks.Models;

[Index("Username", IsUnique = true)]
public class User
{
    public int UserId { get; set; }
    // Definindo quantidade mínima e máxima de caracteres no campo do nome do usuário
    [MinLength(5), MaxLength(30)] public string Username { get; set; } = null!;
    // Definindo quantidade máxima de caracteres no campo do email do usuário
    [MaxLength(256)] public string Email { get; set; } = null!;
    // Definindo quantidade mínima e máxima de caracteres no campo da senha do usuário
    [MinLength(5), MaxLength(30)] public string Password { get; set; } = null!;
    // Navegação de tarefas relacionadas a um usuário em específico
    public ICollection<Task>? Tasks { get; set; }
    // Navegação de rótulos/etiquetas relacionadas a um usuário em específico
    public ICollection<Label>? Labels { get; set; }
    // Não o removê fisicamente do banco de dados, permitindo que se recuperado o usuário.
    public bool Delete { get; set; } = false;
}


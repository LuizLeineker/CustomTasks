using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CustomTasks.Models;

[Index("Username", IsUnique = true), Index("Email", IsUnique = true)]
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
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    // Navegação de rótulos/etiquetas relacionadas a um usuário em específico
    public List<Label> Labels { get; set; } = new List<Label>();
}
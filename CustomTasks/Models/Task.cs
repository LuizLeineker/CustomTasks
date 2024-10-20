using System.ComponentModel.DataAnnotations;

namespace CustomTasks.Models;

public class Task
{
    public int TaskId { get; set; }
    [MinLength(5), MaxLength(30)] public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    // Configurando o relacionamento (chave estrangeira e navegação) com a tabela de usuários (Users)
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    // Navegação de rótulos pertencentes a uma tarefa específica
    public ICollection<Label>? Labels { get; set; }
}
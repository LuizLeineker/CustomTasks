using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CustomTasks.Models;

public class Task
{
    public int TaskId { get; set; }
    [MinLength(5), MaxLength(30)] public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;  
    public DateTime CreatedAt { get; set; }
    // Configurando o relacionamento (chave estrangeira e navegação) com a tabela de usuários (Users)
    public int UserId { get; set; }
    // Navegação de rótulos pertencentes a uma tarefa específica
    [JsonIgnore]
    public ICollection<Label>? Labels { get; set; }
}
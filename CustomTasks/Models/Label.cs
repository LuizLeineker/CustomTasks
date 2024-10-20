using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CustomTasks.Models;

// Definindo a composição de nome de rótulo e usuário como única
[Index("LabelName", "UserId", IsUnique = true)]
public class Label
{
    public int LabelId { get; set; }
    [MinLength(5), MaxLength(30)] public string LabelName { get; set; } = null!;
    // Cofnigurando relacionamento (chave estrangeira e navegação) com a tabela de usuáiros (Users)
    public int UserId { get; set; }
    // Navegação de tarefas com um rótulo específico
    [JsonIgnore]
    public ICollection<Task>? Tasks { get; set; }
}
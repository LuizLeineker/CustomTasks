using System.ComponentModel.DataAnnotations;

namespace CustomTasks.Models;

public class Label
{
    public int LabelId { get; set; }
    [Required] public string LabelName { get; set; } = null!;

/* Configurações de relacionamentos entre usuários (tabela "Users" e rótulos (tabela "Labels")
    public int? UserId { get; set; }
    public User? User { get; set; }
*/
    public ICollection<Task>? Tasks { get; set; }
}

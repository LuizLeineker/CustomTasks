using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomTasks.Models;

public class Task
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [DefaultValue(false)] public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public ICollection<Label>? Labels { get; set; }

    /* Colunas necessárias para relacionar a tabela de tarefas com a de usuários

    public User User { get; set; }
    public int OwnerId { get; set; }
    */
}

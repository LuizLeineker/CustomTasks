namespace CustomTasks.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    // public ICollection<Label>? Labels { get; set; }

    /* Colunas necessárias para relacionar a tabela de tarefas com a de usuários

    public User User { get; set; }
    public int OwnerId { get; set; }
    */
}

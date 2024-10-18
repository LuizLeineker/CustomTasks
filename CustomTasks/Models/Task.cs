using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomTasks.Models;

public class Task
{
    public int Id { get; set; }
    [MinLength(5), MaxLength(20)] public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    // public ICollection<Label>? Labels { get; set; }
}

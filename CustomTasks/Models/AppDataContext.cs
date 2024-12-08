using Microsoft.EntityFrameworkCore;

namespace CustomTasks.Models;

public class AppDataContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;
    public DbSet<Label> Labels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=customtasks.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definindo etiquetas/rótulos padrões disponíveis para qualquer usuário
        modelBuilder.Entity<Label>()
        .HasData(
            // Etiquetas padrão de prioridade
            new Label {LabelId = 1, LabelName = "urgent", IsDefault = true},
            new Label {LabelId = 2, LabelName = "low priority", IsDefault = true},

            // Etiquetas padrão de produtividade
            new Label {LabelId = 3, LabelName = "work", IsDefault = true},
            new Label {LabelId = 4, LabelName = "project", IsDefault = true},
            new Label {LabelId = 5, LabelName = "meeting", IsDefault = true},

            // Etiquetas padrão de aprendizado
            new Label {LabelId = 6, LabelName = "study", IsDefault = true},
            new Label {LabelId = 7, LabelName = "review", IsDefault = true},

            // Outras etiquetas padrão
            new Label {LabelId = 8, LabelName = "personal", IsDefault = true},
            new Label {LabelId = 9, LabelName = "health", IsDefault = true},
            new Label {LabelId = 10, LabelName = "housework", IsDefault = true},
            new Label {LabelId = 11, LabelName = "workout", IsDefault = true}
            
        );

        // Definindo falso como valor padrão da coluna de status para cada nova tarefa
        modelBuilder.Entity<Task>()
        .Property(t => t.IsCompleted)
        .HasDefaultValue(false);

        // Definindo a data e hora atuais do sistema como valor padrão da coluna de data de criação para cada nova tarefa
        modelBuilder.Entity<Task>()
        .Property(t => t.CreatedAt)
        .HasDefaultValueSql("datetime('now', 'localtime')");
    }
}
using Microsoft.EntityFrameworkCore;

namespace CustomTasks.Models;

public class AppDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Label> Labels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=customtasks.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
using Microsoft.EntityFrameworkCore;

namespace CustomTasks.Models;

public class AppDataContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Label> Labels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=customtasks.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Estabelecendo a obrigatoriedade de um nome para cada tarefa adicionada
        modelBuilder.Entity<Task>()
        .Property(t => t.Name)
        .IsRequired();

        // Configurando o valor padrão da coluna de status ("Is completed") para cada nova tarefa adicionada 
        modelBuilder.Entity<Task>()
        .Property(t => t.IsCompleted)
        .HasDefaultValue(false);

        // Configurando o valor padrão da coluna de data de criação ("Created At") para cada nova tarefa adicionada, a data e hora atuais do sistema
        modelBuilder.Entity<Task>()
        .Property(t => t.CreatedAt)
        .HasDefaultValueSql("datetime('now', 'localtime')");
    }
}

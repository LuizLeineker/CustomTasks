using System.ComponentModel.DataAnnotations.Schema;

namespace CustomTasks.Models;

/*  Modelo da tabela de tarefas:
        Colunas:
            id (chave primária), 
            nome,
            rótulo (chave estrangeira),
            descrição, 
            status, 
            data de criação,
            id_dono (chave estrangeira)
*/
public class Task
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

    /* Colunas necessárias para relacionar a tabela de tarefas com a de rótulos/categorias

    public Label Label { get; set; }
    public int LabelId { get; set; } 

    */

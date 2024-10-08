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

    /* Colunas necessárias para relacionar a tabela de tarefas com a de rótulos/categorias

    public Label Label { get; set; }
    public int LabelId { get; set; } 

    */
    
    /* Colunas necessárias para relacionar a tabela de tarefas com a de usuários

    public User User { get; set; }
    public int OwnerId { get; set; }
    */
}

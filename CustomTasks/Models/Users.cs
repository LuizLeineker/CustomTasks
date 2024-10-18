using System.ComponentModel.DataAnnotations;

namespace CustomTasks.Models;

    public class User
    {
        public int Id { get; set; }
        // Definindo quantidade mínima e máxima de caracteres no campo do nome do usuário
        [MinLength(5), MaxLength(30)] public string username { get; set; } = null!;
        // Definindo quantidade máxima de caracteres no campo do email do usuário
        [MaxLength(256)] public string email { get; set; } = null!;
        // Definindo quantidade mínima e máxima de caracteres no campo da senha do usuário
        [MinLength(5), MaxLength(30)] public string password { get; set; } = null!;
        public ICollection<Task>? Tasks { get; set; }
        // public ICollection<Label> Labels { get; set; }
    }

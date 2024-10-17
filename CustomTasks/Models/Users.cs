namespace User.Models;

    public class User
    {
        public string? username { get; set; } = null!;
        public string? email { get; set; } = null!
        public string? password { get; set; } = null!;
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Label> Labels { get; set; }
    }

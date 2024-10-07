using System;
using Microsoft.EntityFrameworkCore;

namespace CustomTasks.Models;

public class AppDataContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=customtasks.db");
    }
}

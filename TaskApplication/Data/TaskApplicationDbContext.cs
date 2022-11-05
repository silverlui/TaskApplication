using Microsoft.EntityFrameworkCore;
using TaskApplication.Data.Models;

namespace TaskApplication.Data;

public class TaskApplicationDbContext : DbContext
{

    public TaskApplicationDbContext(DbContextOptions<TaskApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Todo> Todos => Set<Todo>();
}
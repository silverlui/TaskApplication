using Microsoft.EntityFrameworkCore;
using TaskApplication.Data.Models;

namespace TaskApplication.Data;

public class TaskApplicationDbContext : DbContext
{

    public TaskApplicationDbContext(DbContextOptions<TaskApplicationDbContext> options) : base(options) {}

    public DbSet<Todo> Todos => Set<Todo>();
}
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementSystem.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> op)
        : base(op)
    {

    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
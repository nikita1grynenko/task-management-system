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
    public DbSet<UserTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}
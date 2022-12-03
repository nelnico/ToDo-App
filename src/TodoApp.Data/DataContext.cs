using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;

namespace TodoApp.Data;

public class DataContext : IdentityDbContext<AppUser>
{
    private readonly IServiceProvider _serviceProvider;

    public DataContext(DbContextOptions<DataContext> options, IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider = serviceProvider;
    }

    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
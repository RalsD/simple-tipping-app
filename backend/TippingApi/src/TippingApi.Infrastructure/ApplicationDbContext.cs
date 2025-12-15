using Microsoft.EntityFrameworkCore;
using TippingApi.Domain.Employees;
using TippingApi.Domain.Shifts;
using TippingApi.Domain.Tips;

namespace TippingApi.Infrastructure;


public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<Tip> Tips => Set<Tip>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

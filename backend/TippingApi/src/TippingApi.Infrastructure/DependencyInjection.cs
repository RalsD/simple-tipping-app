using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TippingApi.Domain.Employees;
using TippingApi.Domain.Shifts;
using TippingApi.Domain.Tips;
using TippingApi.Infrastructure.Persistence;

namespace TippingApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("TippingDb");
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IShiftRepository, ShiftRepository>();
        services.AddScoped<ITipRepository, TipRepository>();

        return services;
    }

    public static void SeedDatabase(this IServiceProvider serviceProvider)
    {
        SeedData.Seed(serviceProvider);
    }
}

using Microsoft.Extensions.DependencyInjection;
using TippingApi.Domain.Employees;
using TippingApi.Domain.Shifts;
using TippingApi.Domain.Tips;

namespace TippingApi.Infrastructure.Persistence;

internal static class SeedData
{
    internal static void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        if (context.Employees.Any())
        {
            return;
        }

        var alice = Employee.Create(
            new FirstName("Alice"),
            new LastName("Smith"));


        var bob = Employee.Create(
            new FirstName("Bob"),
            new LastName("Jones"));

        context.Employees.AddRange(alice, bob);

        context.Shifts.AddRange(
            Shift.Create(
                alice,
                DateTime.Today,
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(17)),

            Shift.Create(
                bob,
                DateTime.Today,
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(14)));

        context.Tips.Add(
            Tip.CreateWeekly(DateTime.Today, 300m));

        context.SaveChanges();
    }
}

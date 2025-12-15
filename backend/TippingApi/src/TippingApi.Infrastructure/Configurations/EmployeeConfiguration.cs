using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TippingApi.Domain.Employees;

internal sealed class EmployeeConfiguration
    : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(empl => empl.FirstName)
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new FirstName(value));

        builder.Property(empl => empl.LastName)
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new LastName(value));

        builder.HasMany(x => x.Shifts)
            .WithOne(x => x.Employee);
    }
}


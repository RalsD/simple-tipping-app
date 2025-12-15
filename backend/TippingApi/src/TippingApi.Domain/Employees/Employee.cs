using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Shifts;

namespace TippingApi.Domain.Employees;

public class Employee : Entity
{
    private Employee(
        Guid id,
        FirstName firstName,
        LastName lastName)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public static Employee Create(
        FirstName firstName,
        LastName lastName)
    {
        var employee = new Employee(Guid.NewGuid(), firstName, lastName);
        return employee;
    }

    public void SetFirstName(FirstName firstName)
    {
        if (firstName is null)
            throw new ArgumentNullException(nameof(firstName));

        if (FirstName == firstName) return;

        FirstName = firstName;
    }

    public void SetLastName(LastName lastName)
    {
        if (lastName is null)
            throw new ArgumentNullException(nameof(lastName));

        if (LastName == lastName) return;

        LastName = lastName;
    }
}

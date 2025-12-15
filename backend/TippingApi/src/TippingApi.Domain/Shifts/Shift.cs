using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Employees;

namespace TippingApi.Domain.Shifts;

public class Shift : Entity
{
    protected Shift() { }

    private Shift(
        Guid id,
        Employee employee,
        DateTime date,
        TimeSpan start,
        TimeSpan end)
        : base(id)
    {
        EmployeeId = employee.Id;
        Employee = employee;
        Date = date;
        StartTime = start;
        EndTime = end;
    }

    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }

    public static Shift Create(
        Employee employee,
        DateTime date,
        TimeSpan start,
        TimeSpan end)
    {
        return new Shift(Guid.NewGuid(), employee, date, start, end);
    }

    public double HoursWorked() => (EndTime - StartTime).TotalHours;
}

namespace TippingApi.Domain.Abstractions;

//We could create custom domain entity errors respectively
public record Error(string Code, string Name)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error: NullValue", "Null value was provided");
    public static Error NotFound = new("Error: NotFound", "Entity was not found");
    public static Error NoShiftsForWeek = new("Error: NoShiftsForWeek", "Employee has no shifts for the week");
    public static Error NoHoursWorked = new("Error: NoHoursWorked", "Employee worked no hours this week");
}

namespace TippingApi.Application.Shifts;

public record ShiftResponse(
    Guid Id,
    Guid EmployeeId,
    string EmployeeFirstName,
    string EmployeeLastName,
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    double HoursWorked);


using TippingApi.Application.Shifts;

namespace TippingApi.Application.Employees;

public record EmployeeResponse(
    Guid Id,
    string FirstName,
    string LastName,
    List<ShiftResponse> Shifts);

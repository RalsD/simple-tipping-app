using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Employees.UpdateEmployee;

public record UpdateEmployeeCommand(
    Guid EmployeeId,
    string FirstName,
    string LastName
) : ICommand;


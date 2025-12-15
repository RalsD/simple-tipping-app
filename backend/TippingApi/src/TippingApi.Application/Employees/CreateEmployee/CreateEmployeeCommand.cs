using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Employees.CreateEmployee;

public record CreateEmployeeCommand(string FirstName, string LastName)
    : ICommand<Guid>;

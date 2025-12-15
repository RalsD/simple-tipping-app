using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Employees.GetEmployee;

public record GetEmployeeByIdQuery(Guid EmployeeId) : IQuery<EmployeeResponse>;


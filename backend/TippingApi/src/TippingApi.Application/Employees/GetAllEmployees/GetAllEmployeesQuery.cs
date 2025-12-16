using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Application.DTOs;

namespace TippingApi.Application.Employees.GetAllEmployees;

public sealed record GetAllEmployeesQuery()
    : IQuery<IReadOnlyList<EmployeeDto>>;


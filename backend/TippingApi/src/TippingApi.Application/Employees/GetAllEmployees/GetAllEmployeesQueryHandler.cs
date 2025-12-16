using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Application.DTOs;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Employees;

namespace TippingApi.Application.Employees.GetAllEmployees;

internal sealed class GetAllEmployeesQueryHandler
    : IQueryHandler<GetAllEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<IReadOnlyList<EmployeeDto>>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {

        var employees = await _employeeRepository.GetAllAsync();

        var result = employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            FirstName = e.FirstName.Value,
            LastName = e.LastName.Value,
            Shifts = e.Shifts.Select(s => new ShiftDto
            {
                Id = s.Id,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                HoursWorked = s.HoursWorked()
            }).ToList()
        }).ToList();

        return Result.Success<IReadOnlyList<EmployeeDto>>(result);
    }
}


using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Application.Shifts;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Employees;

namespace TippingApi.Application.Employees.GetEmployee;

public sealed class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, EmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<EmployeeResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee == null) return Result.Failure<EmployeeResponse>(Error.NotFound);
        
        var response = new EmployeeResponse(
            employee.Id,
            employee.FirstName.Value,
            employee.LastName.Value,
            employee.Shifts.Select(s => new ShiftResponse(
                s.Id,
                employee.Id,
                employee.FirstName.Value,
                employee.LastName.Value,
                s.Date,
                s.StartTime,
                s.EndTime,
                s.HoursWorked()
            )).ToList()
        );

        return Result.Success(response);
    }
}


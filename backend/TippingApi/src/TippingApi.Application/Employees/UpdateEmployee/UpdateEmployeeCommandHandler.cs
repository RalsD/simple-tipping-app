using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Employees;

namespace TippingApi.Application.Employees.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler
    : ICommandHandler<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee is null)
            return Result.Failure(Error.NotFound);

        // Update properties
        employee.SetFirstName(new FirstName(request.FirstName));
        employee.SetLastName(new LastName(request.LastName));

        await _employeeRepository.UpdateAsync(employee);

        return Result.Success();
    }
}

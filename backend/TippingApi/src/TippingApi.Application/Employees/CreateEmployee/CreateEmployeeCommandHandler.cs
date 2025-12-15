using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Employees;

namespace TippingApi.Application.Employees.CreateEmployee;

public sealed class CreateEmployeeCommandHandler
    : ICommandHandler<CreateEmployeeCommand, Guid>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Employee.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName)
        );

        await _employeeRepository.AddAsync(employee);

        return employee.Id;
    }
}



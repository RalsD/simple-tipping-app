using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Employees;
using TippingApi.Domain.Shifts;

namespace TippingApi.Application.Shifts.AddShift;

public sealed class AddShiftCommandHandler : ICommandHandler<AddShiftCommand, Guid>
{
    private readonly IShiftRepository _shiftRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AddShiftCommandHandler(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
    {
        _shiftRepository = shiftRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Guid>> Handle(AddShiftCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee is null)
            return Result.Failure<Guid>(Error.NotFound);

        var shift = Shift.Create(employee, request.Date, request.StartTime, request.EndTime);

        await _shiftRepository.AddAsync(shift);

        return shift.Id;
    }
}


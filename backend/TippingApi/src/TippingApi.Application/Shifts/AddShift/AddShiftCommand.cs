using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Shifts.AddShift;

public record AddShiftCommand(Guid EmployeeId, DateTime Date, TimeSpan StartTime, TimeSpan EndTime)
    : ICommand<Guid>;


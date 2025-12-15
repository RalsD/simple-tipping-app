using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Shifts;

namespace TippingApi.Application.Shifts.GetShiftsForWeek;

public sealed class GetShiftsForWeekQueryHandler : IQueryHandler<GetShiftsForWeekQuery, IReadOnlyList<ShiftResponse>>
{
    private readonly IShiftRepository _shiftRepository;

    public GetShiftsForWeekQueryHandler(IShiftRepository shiftRepository)
    {
        _shiftRepository = shiftRepository;
    }

    public async Task<Result<IReadOnlyList<ShiftResponse>>> Handle(
       GetShiftsForWeekQuery request,
       CancellationToken cancellationToken)
    {
        var shifts = await _shiftRepository.GetShiftsForWeekAsync(
            request.WeekStart,
            request.WeekStart.AddDays(7)
        );

        if (!shifts.Any())
            return Result.Failure<IReadOnlyList<ShiftResponse>>(Error.NoShiftsForWeek);

        var response = shifts.Select(s => new ShiftResponse(
            s.Id,
            s.Employee.Id,
            s.Employee.FirstName.Value,
            s.Employee.LastName.Value,
            s.Date,
            s.StartTime,
            s.EndTime,
            s.HoursWorked()
        )).ToList();

        return Result.Success<IReadOnlyList<ShiftResponse>>(response);
    }
}


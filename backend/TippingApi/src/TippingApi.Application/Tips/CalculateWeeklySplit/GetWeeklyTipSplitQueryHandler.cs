using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Shifts;
using TippingApi.Domain.Tips;

namespace TippingApi.Application.Tips.CalculateWeeklySplit;

public sealed class GetWeeklyTipSplitQueryHandler
    : IQueryHandler<GetWeeklyTipSplitQuery, Dictionary<Guid, decimal>>
{
    private readonly ITipRepository _tipRepository;
    private readonly IShiftRepository _shiftRepository;

    public GetWeeklyTipSplitQueryHandler(ITipRepository tipRepository, IShiftRepository shiftRepository)
    {
        _tipRepository = tipRepository;
        _shiftRepository = shiftRepository;
    }

    public async Task<Result<Dictionary<Guid, decimal>>> Handle(GetWeeklyTipSplitQuery request, CancellationToken cancellationToken)
    {
        var shifts = await _shiftRepository.GetShiftsForWeekAsync(request.WeekStart, request.WeekStart.AddDays(7));

        if (!shifts.Any())
            return Result.Failure<Dictionary<Guid, decimal>>(Error.NoShiftsForWeek);

        var totalTips = await _tipRepository.GetWeeklyTotalAsync(request.WeekStart, request.WeekStart.AddDays(7));

        var totalHoursPerEmployee = shifts
            .GroupBy(s => s.EmployeeId)
            .ToDictionary(g => g.Key, g => g.Sum(s => s.HoursWorked()));

        var totalHours = totalHoursPerEmployee.Values.Sum();
        if (totalHours == 0)
            return Result.Failure<Dictionary<Guid, decimal>>(Error.NoHoursWorked);

        var split = totalHoursPerEmployee.ToDictionary(
            kvp => kvp.Key,
            kvp => Math.Round(totalTips * (decimal)(kvp.Value / totalHours), 2)
        );

        return split;
    }
}

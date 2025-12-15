using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Shifts.GetShiftsForWeek;

public record GetShiftsForWeekQuery(DateTime WeekStart) : IQuery<IReadOnlyList<ShiftResponse>>;


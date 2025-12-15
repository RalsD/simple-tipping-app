using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Tips.CalculateWeeklySplit;

public record GetWeeklyTipSplitQuery(DateTime WeekStart) : IQuery<Dictionary<Guid, decimal>>;


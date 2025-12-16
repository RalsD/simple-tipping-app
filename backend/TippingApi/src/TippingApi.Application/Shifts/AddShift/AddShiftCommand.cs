using System.Text.Json.Serialization;
using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Application.Shared.JsonConverters;

namespace TippingApi.Application.Shifts.AddShift;

public record AddShiftCommand(
    Guid EmployeeId,
    DateTime Date,
    [property: JsonConverter(typeof(TimeSpanConverter))]
    TimeSpan StartTime,
    [property: JsonConverter(typeof(TimeSpanConverter))]
    TimeSpan EndTime
) : ICommand<Guid>;


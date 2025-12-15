using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Shifts.GetShiftById;

public record GetShiftByIdQuery(Guid ShiftId) : IQuery<ShiftResponse>;

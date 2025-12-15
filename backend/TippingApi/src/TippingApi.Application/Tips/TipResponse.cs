namespace TippingApi.Application.Tips;

public record TipResponse(
    Guid Id,
    decimal Amount,
    DateTime WeekStart
);


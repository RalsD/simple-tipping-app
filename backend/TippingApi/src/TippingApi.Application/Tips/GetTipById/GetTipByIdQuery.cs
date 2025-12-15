using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Tips.GetTipById;

public record GetTipByIdQuery(Guid TipId) : IQuery<TipResponse>;

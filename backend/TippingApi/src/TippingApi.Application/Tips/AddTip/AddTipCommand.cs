using TippingApi.Application.Abstractions.Messaging;

namespace TippingApi.Application.Tips.AddTip;

public record AddTipCommand(DateTime Date, decimal Amount) : ICommand<Guid>;


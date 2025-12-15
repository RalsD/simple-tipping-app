using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Tips;

namespace TippingApi.Application.Tips.AddTip;

public sealed class AddTipCommandHandler : ICommandHandler<AddTipCommand, Guid>
{
    private readonly ITipRepository _tipRepository;

    public AddTipCommandHandler(ITipRepository tipRepository)
    {
        _tipRepository = tipRepository;
    }

    public async Task<Result<Guid>> Handle(AddTipCommand request, CancellationToken cancellationToken)
    {
        var tip = Tip.Create(request.Amount, request.Date);
        await _tipRepository.AddAsync(tip);
        return tip.Id;
    }
}


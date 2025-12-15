using TippingApi.Application.Abstractions.Messaging;
using TippingApi.Domain.Abstractions;
using TippingApi.Domain.Tips;

namespace TippingApi.Application.Tips.GetTipById;

public sealed class GetTipByIdQueryHandler : IQueryHandler<GetTipByIdQuery, TipResponse>
{
    private readonly ITipRepository _tipRepository;

    public GetTipByIdQueryHandler(ITipRepository tipRepository)
    {
        _tipRepository = tipRepository;
    }

    public async Task<Result<TipResponse>> Handle(GetTipByIdQuery request, CancellationToken cancellationToken)
    {
        var tip = await _tipRepository.GetByIdAsync(request.TipId);

        if (tip is null)
            return Result.Failure<TipResponse>(Error.NotFound);

        var response = new TipResponse(
            tip.Id,
            tip.Amount,
            tip.Date
        );

        return Result.Success(response);
    }
}

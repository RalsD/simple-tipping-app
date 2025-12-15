using MediatR;
using TippingApi.Domain.Abstractions;

namespace TippingApi.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

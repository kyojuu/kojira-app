using MediatR;
using SharedKernel;

namespace kojira.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

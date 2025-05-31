using MediatR;
using SharedKernel;

namespace kojira.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TRespone> : IRequest<Result<TRespone>>, IBaseCommand;

public interface IBaseCommand;

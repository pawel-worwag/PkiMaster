namespace PkiMaster.Application.Common.Messaging;

public interface IHandler<in TQuery, TResult> where TQuery : IRequest<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}

public interface IHandler<in TQuery> where TQuery : IRequest
{
    Task HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
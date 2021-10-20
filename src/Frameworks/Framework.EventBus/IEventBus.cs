using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.CommandHandlers;
using Framework.Domain.Core;
using Framework.Query;
using MediatR;

namespace Framework.EventBus
{
    public interface IEventBus
    {
        Task<TResponse> Issue<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken = default)
            where TResponse : ResponseCommand;
        Task<TResponse> IssueQuery<TResponse>(IRequest<TResponse> query, CancellationToken cancellationToken = default)
            where TResponse : BaseResponseQuery;
        Task DomainEventDispatcher<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent;

    }
}
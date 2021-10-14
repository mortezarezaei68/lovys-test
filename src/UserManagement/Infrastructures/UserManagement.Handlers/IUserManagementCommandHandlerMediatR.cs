using System;
using Framework.Commands.CommandHandlers;
using MediatR;

namespace UserManagement.Handlers
{
    public interface IUserManagementCommandHandlerMediatR<in TUserCommandRequest, TUserCommandResponse> :
        ITransactionalCommandHandlerMediatR<TUserCommandRequest, TUserCommandResponse>
        where TUserCommandResponse : ResponseCommand where TUserCommandRequest : IRequest<TUserCommandResponse>
    {
    }
}
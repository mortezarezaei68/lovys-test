using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using UserManagement.Persistence.EF.UnitOfWork;

namespace UserManagement.Handlers
{
    public class UserManagementCommandHandlerMediatR<TUserCommandRequest,TUserCommandResponse>: 
        IUserManagementCommandHandlerMediatR<TUserCommandRequest,TUserCommandResponse> where TUserCommandResponse:ResponseCommand where TUserCommandRequest:IRequest<TUserCommandResponse>
    {
        private readonly ILogger<UserManagementCommandHandlerMediatR<TUserCommandRequest,TUserCommandResponse>> _logger;
        private readonly IIdentityUnitOfWork _unitOfWork;

        public UserManagementCommandHandlerMediatR(
            ILogger<UserManagementCommandHandlerMediatR<TUserCommandRequest, TUserCommandResponse>> logger,
            IIdentityUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<TUserCommandResponse> Handle(TUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TUserCommandResponse> next)
        {
            if (request is IUserManagementRequest<TUserCommandResponse>)
            {
                try
                {
                    if (_unitOfWork.HasActiveTransaction) return await next();
                    await using var transaction = await _unitOfWork.BeginTransactionAsync();
                    var response = await next();
                    await _unitOfWork.CommitAsync(transaction);
                    _logger.LogInformation(_unitOfWork.GetType().ToString());
                    return response;
                }
                catch (AppException ex)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new AppException(ResultCode.BadRequest,ex.Message);
                }
            }

            var nextResponse = await next();
            return nextResponse;

        }
    }
}
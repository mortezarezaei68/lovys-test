using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using ScheduleManagement.Persistence.EF.UnitOfWork;

namespace ScheduleManagement.Handlers
{
    public class ScheduleManagementCommandHandlerMediatR<TScheduleCommandRequest, TCommandResponse> :
        IScheduleManagementCommandHandlerMediatR<TScheduleCommandRequest, TCommandResponse>
        where TCommandResponse : ResponseCommand where TScheduleCommandRequest : IRequest<TCommandResponse>
    {
        private readonly ILogger<ScheduleManagementCommandHandlerMediatR<TScheduleCommandRequest, TCommandResponse>>
            _logger;

        private readonly IScheduleManagementUnitOfWork _unitOfWork;

        public ScheduleManagementCommandHandlerMediatR(IScheduleManagementUnitOfWork unitOfWork,
            ILogger<ScheduleManagementCommandHandlerMediatR<TScheduleCommandRequest, TCommandResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TCommandResponse> Handle(TScheduleCommandRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<TCommandResponse> next)
        {
            if (request is IScheduleManagementRequest<TCommandResponse>)
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
                    throw new AppException(ResultCode.BadRequest, ex.Message);
                }
            }

            var responseCommand = await next();
            return responseCommand;
        }
    }
}
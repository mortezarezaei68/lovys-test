using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.EventBus;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.Extensions.Configuration;
using UserManagement.Commands.AdminUserCommands;
using UserManagement.Commands.UserTokenCommands;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.InterviewerUserCommandHandlers
{
    public class SignOutCommandHandler:IUserManagementCommandHandlerMediatR<SignOutAdminUserCommandRequest,SignOutAdminUserCommandResponse>
    {
        private readonly IEventBus _eventBus;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;

        public SignOutCommandHandler(IEventBus eventBus, ICurrentUser currentUser, IConfiguration configuration)
        {
            _eventBus = eventBus;
            _currentUser = currentUser;
            _configuration = configuration;
        }

        public async Task<SignOutAdminUserCommandResponse> Handle(SignOutAdminUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<SignOutAdminUserCommandResponse> next)
        {

            var userId = _currentUser.GetUserId();
            if (userId is null)
                return new SignOutAdminUserCommandResponse(false, ResultCode.UnAuthorized);
                
            
            var persistGrant = new InvokePersistGrantsCommandRequest
            {
                UserId = userId
            };
            await _eventBus.Issue(persistGrant, cancellationToken);
            _currentUser.CleanSecurityCookie("X-Access-Token",_configuration["JwtToken:DomainUrl"]);
            _currentUser.CleanSecurityCookie("X-Refresh-Token",_configuration["JwtToken:DomainUrl"]);
            return new SignOutAdminUserCommandResponse(true, ResultCode.Success);
        }
    }
}
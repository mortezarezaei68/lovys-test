using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.EventBus;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserManagement.Commands.AdminUserCommands;
using UserManagement.Commands.UserTokenCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.InterviewerUserCommandHandlers
{
    public class LoginInterviewerUserCommandHandler:IUserManagementCommandHandlerMediatR<LoginAdminUserCommandRequest,LoginAdminUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEventBus _eventBus;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;

        public LoginInterviewerUserCommandHandler(UserManager<User> userManager, IEventBus eventBus, ICurrentUser currentUser, IConfiguration configuration)
        {
            _userManager = userManager;
            _eventBus = eventBus;
            _currentUser = currentUser;
            _configuration = configuration;
        }

        public async Task<LoginAdminUserCommandResponse> Handle(LoginAdminUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<LoginAdminUserCommandResponse> next)
        {
            var adminUser = await _userManager.FindByNameAsync(request.UserName);
            if (adminUser is null || adminUser.IsDeleted)
                throw new AppException(ResultCode.BadRequest, "user password or username is not correct");
            
            var passwordChecker = await _userManager.CheckPasswordAsync(adminUser, request.Password);
            if (!passwordChecker)
                throw new AppException(ResultCode.BadRequest, "user password or username is not correct");
            
            var userRoles = await _userManager.GetRolesAsync(adminUser);
            var command = new AccessTokenCommandRequest
            {
                Roles = userRoles.ToList(),
                SubjectId = adminUser.SubjectId.ToString(),
                UserType = UserType.Interviewer,
                UserId = adminUser.Id
            };
            var userToken=await _eventBus.Issue(command, cancellationToken);
            _currentUser.SetHttpOnlyUserCookie("X-Access-Token", userToken.Data.AccessToken,
                DateTimeOffset.Now.AddSeconds(int.Parse(_configuration["JwtToken:AccessTokenExpiredTime"])),
                _configuration["JwtToken:DomainUrl"]);
            
            _currentUser.SetHttpOnlyUserCookie("X-Refresh-Token", userToken.Data.RefreshToken,
                DateTimeOffset.Now.AddDays(int.Parse(_configuration["JwtToken:ExpirationDays"])), _configuration["JwtToken:DomainUrl"]);
            
            return new LoginAdminUserCommandResponse(true, ResultCode.Success);
        }
    }
}
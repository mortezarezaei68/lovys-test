using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.EventBus;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserManagement.Commands.CandidateUserCommands;
using UserManagement.Commands.UserTokenCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.CandidateCommandHandlers
{
    public class LoginCandidateUserCommandHandler:IUserManagementCommandHandlerMediatR<LoginCandidateUserCommandRequest,LoginCandidateUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEventBus _eventBus;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;

        public LoginCandidateUserCommandHandler(UserManager<User> userManager, IEventBus eventBus, ICurrentUser currentUser, IConfiguration configuration)
        {
            _userManager = userManager;
            _eventBus = eventBus;
            _currentUser = currentUser;
            _configuration = configuration;
        }
        

        public async Task<LoginCandidateUserCommandResponse> Handle(LoginCandidateUserCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<LoginCandidateUserCommandResponse> next)
        {
            var customerUser = await _userManager.FindByNameAsync(request.UserName);
            if (customerUser is null || customerUser.IsDeleted) 
                throw new AppException(ResultCode.BadRequest, "user password or username is not correct");
            
            var passwordChecker = await _userManager.CheckPasswordAsync(customerUser, request.Password);
            if (!passwordChecker)
                throw new AppException(ResultCode.BadRequest, "user password or username is not correct");
            
            var command = new AccessTokenCommandRequest
            {
                SubjectId = customerUser.SubjectId.ToString(),
                UserType = UserType.Candidate,
                UserId = customerUser.Id
            };
            var userToken=await _eventBus.Issue(command, cancellationToken);

            _currentUser.SetHttpOnlyUserCookie("X-Access-Token", userToken.Data.AccessToken,
                DateTimeOffset.Now.AddSeconds(int.Parse(_configuration["JwtToken:AccessTokenExpiredTime"])),
                _configuration["JwtToken:DomainUrl"]);
            
            _currentUser.SetHttpOnlyUserCookie("X-Refresh-Token", userToken.Data.RefreshToken,
                DateTimeOffset.Now.AddDays(int.Parse(_configuration["JwtToken:ExpirationDays"])), _configuration["JwtToken:DomainUrl"]);
            
            return new LoginCandidateUserCommandResponse(true, ResultCode.Success);
        }
    }
}
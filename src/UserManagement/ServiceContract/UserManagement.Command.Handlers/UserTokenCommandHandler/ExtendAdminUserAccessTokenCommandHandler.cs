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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserManagement.Commands.UserTokenCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.UserTokenCommandHandler
{
    public class ExtendAdminUserAccessTokenCommandHandler:IUserManagementCommandHandlerMediatR<ExtendAccessTokenCommandRequest,ExtendAccessTokenCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEventBus _eventBus;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;

        public ExtendAdminUserAccessTokenCommandHandler(UserManager<User> userManager, IEventBus eventBus, ICurrentUser currentUser, IConfiguration configuration)
        {
            _userManager = userManager;
            _eventBus = eventBus;
            _currentUser = currentUser;
            _configuration = configuration;
        }

        public async Task<ExtendAccessTokenCommandResponse> Handle(ExtendAccessTokenCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ExtendAccessTokenCommandResponse> next)
        {
            var user = await _userManager.Users.Include(a => a.PersistGrants).Where(a =>
                a.PersistGrants.Any(b => b.RefreshToken == request.RefreshToken)).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var existedPersist = user.PersistGrants.FirstOrDefault(a => a.RefreshToken == request.RefreshToken);
            if (existedPersist is not null && !existedPersist.IsActive)
                throw new AppException(ResultCode.UnAuthorized, "your refresh token InActivated");

            if (existedPersist is not null && existedPersist.IsExpired)
                throw new AppException(ResultCode.UnAuthorized, "your refresh Token is Expired");
            if (user is null)
                throw new AppException(ResultCode.UnAuthorized, "Your RefreshToken Not Valid");

            var userRoles = await _userManager.GetRolesAsync(user);
            var command = new AccessTokenCommandRequest
                {
                    Roles = userRoles.ToList(),
                    SubjectId = user.SubjectId.ToString(),
                    UserType = UserType.Interviewer,
                    UserId = user.Id
                };
            var userToken=await _eventBus.Issue(command, cancellationToken);
            
            _currentUser.SetHttpOnlyUserCookie("X-Access-Token", userToken.Data.AccessToken,
                    DateTimeOffset.Now.AddSeconds(int.Parse(_configuration["JwtToken:AccessTokenExpiredTime"])),
                    _configuration["JwtToken:DomainUrl"]);
            
            _currentUser.SetHttpOnlyUserCookie("X-Refresh-Token", userToken.Data.RefreshToken,
                    DateTimeOffset.Now.AddDays(int.Parse(_configuration["JwtToken:ExpirationDays"])), _configuration["JwtToken:DomainUrl"]);
            var tokens = new ExtendRefreshTokenViewModel
            {
                AccessToken = userToken.Data.AccessToken,
                RefreshToken = userToken.Data.RefreshToken
            };
            return new ExtendAccessTokenCommandResponse(true, ResultCode.Success,tokens);
            
        }
    }
}
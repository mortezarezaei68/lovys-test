using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.EventBus;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.Extensions.Configuration;
using UserManagement.Commands.UserTokenCommands;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.UserTokenCommandHandler
{
    public class CreateRefreshTokenCommandHandler:IUserManagementCommandHandlerMediatR<RefreshTokenCommandRequest,RefreshTokenCommandResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IEventBus _eventBus;
        private readonly IConfiguration _configuration;
        private static Random random = new();
        public CreateRefreshTokenCommandHandler(ICurrentUser currentUser, IEventBus eventBus, IConfiguration configuration)
        {
            _currentUser = currentUser;
            _eventBus = eventBus;
            _configuration = configuration;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<RefreshTokenCommandResponse> next)
        {
            
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
           var refreshToken= new string(Enumerable.Repeat(chars, 64)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var persistGrant = new AddPersistGrantsCommandRequest()
            {
                ExpiredTime = DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtToken:ExpirationDays"])),
                IpAddress = _currentUser.GetUserIp(),
                RefreshToken = refreshToken,
                SubjectId = request.SubjectId,
                UserId = request.UserId,
                UserType = request.UserType
            };
            await _eventBus.Issue(persistGrant);
            var refreshTokenViewModel = new RefreshTokenViewModel
            {
                RefreshToken = persistGrant.RefreshToken
            };
            return new RefreshTokenCommandResponse(true, ResultCode.Success, refreshTokenViewModel);
        }
    }
}
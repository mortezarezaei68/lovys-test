using System.Threading;
using System.Threading.Tasks;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Commands.UserTokenCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.UserTokenCommandHandler
{
    public class AddPersistGrantsCommandHandler:IUserManagementCommandHandlerMediatR<AddPersistGrantsCommandRequest,AddPersistGrantsCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public AddPersistGrantsCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddPersistGrantsCommandResponse> Handle(AddPersistGrantsCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddPersistGrantsCommandResponse> next)
        {
            var user = await _userManager.Users.Include(a => a.PersistGrants)
                .FirstOrDefaultAsync(a => a.Id == request.UserId, cancellationToken: cancellationToken);
            user.UpdatePersistGrants();
            user.AddPersistGrant(request.SubjectId,request.RefreshToken,request.IpAddress,request.ExpiredTime,request.UserType);
            await _userManager.UpdateAsync(user);
            return new AddPersistGrantsCommandResponse(true, ResultCode.Success);
        }
    }
}
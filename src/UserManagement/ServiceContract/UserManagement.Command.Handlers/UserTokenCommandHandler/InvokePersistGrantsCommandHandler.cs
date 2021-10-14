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
    public class InvokePersistGrantsCommandHandler:IUserManagementCommandHandlerMediatR<InvokePersistGrantsCommandRequest,InvokePersistGrantsCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public InvokePersistGrantsCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<InvokePersistGrantsCommandResponse> Handle(InvokePersistGrantsCommandRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<InvokePersistGrantsCommandResponse> next)
        {
            var user = await _userManager.Users.Include(a => a.PersistGrants)
                .FirstOrDefaultAsync(a => a.SubjectId.ToString() == request.UserId, cancellationToken: cancellationToken);
            user.UpdatePersistGrants();
            return new InvokePersistGrantsCommandResponse(true, ResultCode.Success);
        }
    }
}
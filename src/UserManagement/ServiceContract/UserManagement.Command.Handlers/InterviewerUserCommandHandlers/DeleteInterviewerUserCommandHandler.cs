using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.EventBus;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Commands.AdminUserCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.InterviewerUserCommandHandlers
{
    public class DeleteInterviewerUserCommandHandler : IUserManagementCommandHandlerMediatR<DeleteInterviewerUserCommandRequest,
        DeleteAdminUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public DeleteInterviewerUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DeleteAdminUserCommandResponse> Handle(DeleteInterviewerUserCommandRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<DeleteAdminUserCommandResponse> next)
        {
            var user = await _userManager.Users.Include(a => a.PersistGrants)
                .FirstOrDefaultAsync(a => a.Id == Convert.ToInt32(request.Id), cancellationToken);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "not found user");

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                 await _userManager.RemoveFromRolesAsync(user, roles);
            }
            user.Delete();
            await _userManager.UpdateAsync(user);

            return new DeleteAdminUserCommandResponse(true, ResultCode.Success);
        }
    }
}
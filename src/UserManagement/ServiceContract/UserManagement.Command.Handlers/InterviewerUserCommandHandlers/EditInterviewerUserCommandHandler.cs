using System.Threading;
using System.Threading.Tasks;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Commands.AdminUserCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.InterviewerUserCommandHandlers
{
    public class EditInterviewerUserCommandHandler : IUserManagementCommandHandlerMediatR<EditInterviewerUserCommandRequest,
        EditAdminUserCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public EditInterviewerUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<EditAdminUserCommandResponse> Handle(EditInterviewerUserCommandRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<EditAdminUserCommandResponse> next)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");
            user.Update(request.FirstName, request.LastName,
                request.UserName, request.EmailAddress);

            await _userManager.UpdateAsync(user);


            return new EditAdminUserCommandResponse(true, ResultCode.Success);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Commands.CustomerUserCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.CandidateCommandHandlers
{
    public class ConfirmResetPasswordCommandHandler : IUserManagementCommandHandlerMediatR<
        ConfirmResetPasswordCommandRequest, ConfirmResetPasswordCommandResponse>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmResetPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ConfirmResetPasswordCommandResponse> Handle(ConfirmResetPasswordCommandRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<ConfirmResetPasswordCommandResponse> next)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "can not find user");
            var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!resetPassResult.Succeeded)
                throw new AppException(ResultCode.BadRequest, "can not reset your password");
            return new ConfirmResetPasswordCommandResponse(true, ResultCode.Success);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.Commands.UserRoleCommands;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Command.Handlers.RoleCommandHandlers
{
    public class DeleteRoleCommandHandler:IUserManagementCommandHandlerMediatR<DeleteRoleCommandRequest,DeleteRoleCommandResponse>
    {
        private readonly RoleManager<Role> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<DeleteRoleCommandResponse> next)
        {
            var role=await _roleManager.FindByIdAsync(request.Id);
            if (role is null)
                throw new AppException(ResultCode.BadRequest, "can not found role");
            
            role.Delete();
            await _roleManager.UpdateAsync(role);

            return new DeleteRoleCommandResponse(true, ResultCode.Success);
        }
    }
}
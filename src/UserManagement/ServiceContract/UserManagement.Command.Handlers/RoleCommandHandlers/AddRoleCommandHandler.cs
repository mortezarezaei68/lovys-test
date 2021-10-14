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
    public class AddRoleCommandHandler:IUserManagementCommandHandlerMediatR<AddRoleCommandRequest,AddRoleCommandResponse>
    {
        private readonly RoleManager<Role> _roleManager;

        public AddRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<AddRoleCommandResponse> Handle(AddRoleCommandRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<AddRoleCommandResponse> next)
        {

            var existRole = await _roleManager.FindByNameAsync(request.Name);
            if (existRole is not null)
                throw new AppException(ResultCode.BadRequest, "role is existed");
            
            await _roleManager.CreateAsync(new Role {Name = request.Name});
            
            return new AddRoleCommandResponse(true, ResultCode.Success);
        }
    }
}
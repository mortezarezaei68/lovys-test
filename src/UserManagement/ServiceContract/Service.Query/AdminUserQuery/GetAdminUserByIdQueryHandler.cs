using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using Framework.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminRoleQuery;
using Service.Query.Model.AdminUserQuery;
using UserManagement.Domain;

namespace Service.Query.AdminUserQuery
{
    public class GetAdminUserByIdQueryHandler:IQueryHandlerMediatR<GetAdminUserQueryRequest,GetAdminUserQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICurrentUser _currentUser;

        public GetAdminUserByIdQueryHandler(UserManager<User> userManager, RoleManager<Role> roleManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUser = currentUser;
        }

        public async Task<GetAdminUserQueryResponse> Handle(GetAdminUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user=await _userManager.Users.FirstOrDefaultAsync(a=>a.Id==request.UserId);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");
                
            
            var roles = await _userManager.GetRolesAsync(user);
            var adminUser =new AdminUserModel
            {
                Email = user.Email,
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                UserType = user.UserType,
                UserRoles = _roleManager.Roles.Where(a => roles.Any(b => b == a.Name)).Select(a =>
                    new RoleModel()
                    {
                        Name = a.Name,
                        Id = a.Id.ToString()
                    }).ToList()
            };
            return new GetAdminUserQueryResponse(true, adminUser);
        }
    }
}
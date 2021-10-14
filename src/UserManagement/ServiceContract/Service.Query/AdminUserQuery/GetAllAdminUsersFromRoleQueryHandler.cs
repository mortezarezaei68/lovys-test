using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Query;
using Microsoft.AspNetCore.Identity;
using Service.Query.Model.AdminUserQuery;
using UserManagement.Domain;

namespace Service.Query.AdminUserQuery
{
    public class GetAllAdminUsersFromRoleQueryHandler:IQueryHandlerMediatR<GetAllAdminUsersFromRoleQueryRequest,GetAllAdminUsersFromRoleQueryResponse>
    {
        private readonly UserManager<User> _userManager;

        public GetAllAdminUsersFromRoleQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetAllAdminUsersFromRoleQueryResponse> Handle(GetAllAdminUsersFromRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.GetUsersInRoleAsync(request.RoleName);
            var userModel = users.Select(a => new AdminUserModel
            {
                SubjectId = a.SubjectId.ToString(),
                Email = a.Email,
                Id = a.Id.ToString(),
                FirstName = a.FirstName,
                LastName = a.LastName,
                UserName = a.UserName,
                UserType = a.UserType
            }).ToList();

            return new GetAllAdminUsersFromRoleQueryResponse(true, userModel);

        }
    }
}
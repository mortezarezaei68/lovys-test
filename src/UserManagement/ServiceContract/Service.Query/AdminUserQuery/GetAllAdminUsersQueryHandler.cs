using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminUserQuery;
using UserManagement.Domain;

namespace Service.Query.AdminUserQuery
{
    public class GetAllAdminUsersQueryHandler:IGetAllAdminUsersQueryHandler
    {
        private readonly UserManager<User> _userManager;
        public GetAllAdminUsersQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetAllAdminUserQueryResponse> Handle(GetAllAdminUserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Select(
                a => new AdminUserModel
                {
                    SubjectId = a.SubjectId.ToString(),
                    Email = a.Email,
                    Id = a.Id.ToString(),
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    UserName = a.UserName,
                    UserType = a.UserType
                }).ToListAsync(cancellationToken: cancellationToken);
            return new GetAllAdminUserQueryResponse(true, users,users.Count);
        }
    }
}
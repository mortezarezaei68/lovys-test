using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.GetAllUserQuery;
using UserManagement.Domain;

namespace Service.Query.GetAllUserQuery
{
    public class GetAllUsersQueryHandler:IGetAllUsersQueryHandler
    {
        private readonly UserManager<User> _userManager;
        public GetAllUsersQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Select(
                a => new UserModel
                {
                    SubjectId = a.SubjectId.ToString(),
                    Email = a.Email,
                    Id = a.Id.ToString(),
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    UserName = a.UserName,
                    UserType = a.UserType
                }).ToListAsync(cancellationToken: cancellationToken);
            return new GetAllUserQueryResponse(true, users,users.Count);
        }
    }
}
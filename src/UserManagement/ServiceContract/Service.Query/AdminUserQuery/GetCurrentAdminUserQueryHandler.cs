using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using Framework.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminUserQuery;
using UserManagement.Domain;

namespace Service.Query.AdminUserQuery
{
    public class GetCurrentAdminUserQueryHandler:IQueryHandlerMediatR<GetCurrentAdminUserQueryRequest,GetCurrentAdminUserQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetCurrentAdminUserQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<GetCurrentAdminUserQueryResponse> Handle(GetCurrentAdminUserQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUser.GetUserId();
            var user=await _userManager.Users.FirstOrDefaultAsync(a=>a.SubjectId.ToString()==currentUserId && a.UserType==UserType.Interviewer);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");
                
            
            var adminCurrentUser = new AdminCurrentUserModel
            {
                Name = $"{user.FirstName} {user.LastName}"
            };
            return new GetCurrentAdminUserQueryResponse(true, adminCurrentUser);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using Framework.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.AdminUserQuery;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Domain;

namespace Service.Query.AdminUserQuery
{
    public class GetCurrentUserQueryHandler:IGetCurrentUserQueryHandler
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetCurrentUserQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async  Task<GetCurrentUserQueryResponse> Handle(GetCurrentUserQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUser.GetUserId();
            var user = await _userManager.Users.FirstOrDefaultAsync(
                a => a.SubjectId.ToString() == currentUserId ,
                cancellationToken: cancellationToken);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");


            var currentCustomerUser = new AdminUserModel
            {
                Email = user.Email,
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                SubjectId = user.SubjectId.ToString(),
                UserName = user.UserName,
                UserType = user.UserType
            };
            return new GetCurrentUserQueryResponse(true, currentCustomerUser);
        }
        
    }

    public interface IGetCurrentUserQueryHandler:IQueryHandler
    {
        Task<GetCurrentUserQueryResponse> Handle(GetCurrentUserQueryRequest request,
            CancellationToken cancellationToken);
    }
}
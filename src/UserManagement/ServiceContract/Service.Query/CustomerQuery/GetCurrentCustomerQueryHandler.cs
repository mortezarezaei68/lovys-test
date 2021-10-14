using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using Framework.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Domain;

namespace Service.Query.CustomerQuery
{
    public class
        GetCurrentCustomerQueryHandler : IQueryHandlerMediatR<GetCurrentCustomerQueryRequest,
            GetCurrentCustomerQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetCurrentCustomerQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<GetCurrentCustomerQueryResponse> Handle(GetCurrentCustomerQueryRequest request,
            CancellationToken cancellationToken)
        {
            var currentUserId = _currentUser.GetUserId();
            var user = await _userManager.Users.FirstOrDefaultAsync(
                a => a.SubjectId.ToString() == currentUserId && a.UserType == UserType.Candidate,
                cancellationToken: cancellationToken);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");


            var currentCustomerUser = new CurrentCustomerQueryModel
            {
                FullName = $"{user.FirstName} {user.LastName}"
            };
            return new GetCurrentCustomerQueryResponse(true, currentCustomerUser);
        }
    }
}
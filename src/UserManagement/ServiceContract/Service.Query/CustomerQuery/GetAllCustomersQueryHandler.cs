using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Query.Model.CustomerUserQuery;
using UserManagement.Domain;

namespace Service.Query.CustomerQuery
{
    public class GetAllCustomersQueryHandler:IQueryHandlerMediatR<GetAllCustomerQueryRequest,GetAllCustomerQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetAllCustomersQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<GetAllCustomerQueryResponse> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .Where(a => a.UserType == UserType.Candidate).Select(
                    a => new CustomerUserModel
                    {
                        Email = a.Email,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        UserName = a.UserName
                    }).ToListAsync(cancellationToken: cancellationToken);
            return new GetAllCustomerQueryResponse(true, users,users.Count);
        }
    }
}
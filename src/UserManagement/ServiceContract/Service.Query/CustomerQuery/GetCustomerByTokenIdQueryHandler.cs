using System;
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
    public class GetCustomerByTokenIdQueryHandler:IQueryHandlerMediatR<CustomerByTokenIdQueryRequest,CustomerByTokenIdQueryResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetCustomerByTokenIdQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<CustomerByTokenIdQueryResponse> Handle(CustomerByTokenIdQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(a => a.UserType == UserType.Candidate && a.SubjectId.ToString()==userId, cancellationToken: cancellationToken);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");

            var customer=new CustomerUserModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
            };
            return new CustomerByTokenIdQueryResponse(true, customer);
        }
    }
}
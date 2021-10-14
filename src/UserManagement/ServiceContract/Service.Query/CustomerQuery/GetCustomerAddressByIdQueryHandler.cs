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
    public class GetCustomerAddressByIdQueryHandler:IQueryHandlerMediatR<GetCustomerAddressByIdRequest,GetCustomerAddressByIdResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public GetCustomerAddressByIdQueryHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<GetCustomerAddressByIdResponse> Handle(GetCustomerAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(a => a.UserType == UserType.Candidate && a.SubjectId.ToString()==userId, cancellationToken: cancellationToken);
            if (user is null)
                throw new AppException(ResultCode.BadRequest, "user not found");
            return new GetCustomerAddressByIdResponse(true, new CustomerAddressQueryModel());


        }
    }
}
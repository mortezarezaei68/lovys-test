using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using Framework.Queries;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domains;
using OrderManagement.Persistence.EF.Context;
using OrderManagement.Query.Commands;

namespace OrderManagement.Query.Handlers
{
    public class GetOrdersByUserIdQueryHandler:IQueryHandlerMediatR<GetOrdersByUserIdQueryRequest,GetOrdersByUserIdQueryResponse>
    {
        private readonly OrderManagementDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetOrdersByUserIdQueryHandler(OrderManagementDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<GetOrdersByUserIdQueryResponse> Handle(GetOrdersByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var orders =await _context.Orders.Where(a => a.UserId == _currentUser.GetUserId()).ToListAsync(cancellationToken: cancellationToken);
            if (orders is null)
                throw new AppException(ResultCode.BadRequest, "there isn't any order for user");
                
            
            var orderModels = orders.Select(a => new GetOrderByUserIdQueryResponseModel
            {
                Expired = a.Expired,
                IsExpired = a.IsExpired,
                Id = a.Id,
                OrderState = (int) a.OrderState,
                TotalPrice = a.TotalPrice,
                TrackingCode = a.TrackingCode,
                DocumentMailingFee = a.DocumentMailingFee
            });
            return new GetOrdersByUserIdQueryResponse(true, orderModels);

        }
    }
}
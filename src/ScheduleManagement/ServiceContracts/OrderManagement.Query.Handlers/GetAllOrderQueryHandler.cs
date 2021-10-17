using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Buses;
using Framework.Queries;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Persistence.EF.Context;
using OrderManagement.Query.Commands;
using Service.Query.Model.AdminUserQuery;

namespace OrderManagement.Query.Handlers
{
    public class GetAllOrderQueryHandler:IQueryHandlerMediatR<GetAllOrderQueryRequest,GetAllOrderQueryResponse>
    {
        private readonly OrderManagementDbContext _context;
        private readonly IEventBus _eventBus;

        public GetAllOrderQueryHandler(OrderManagementDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task<GetAllOrderQueryResponse> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.ToListAsync(cancellationToken: cancellationToken);
            var users = await _eventBus.IssueQuery(new GetAllAdminUserQueryRequest(), cancellationToken);
            var orderModel = orders.Select(a => new GetAllAdminOrderQueryModel
            {
                Expired = a.Expired,
                IsExpired = a.IsExpired,
                Id = a.Id,
                OrderState = (int) a.OrderState,
                TotalPrice = a.TotalPrice,
                TrackingCode = a.TrackingCode,
                CreatedDateTime = a.CreatedAt,
                DocumentMailingFee = a.DocumentMailingFee,
                FirstName = users.Data.FirstOrDefault(b=>b.SubjectId.ToLower()==a.UserId)?.FirstName,
                LastName = users.Data.FirstOrDefault(b=>b.SubjectId.ToLower()==a.UserId)?.LastName,
                PhoneNumber = users.Data.FirstOrDefault(b=>b.SubjectId.ToLower()==a.UserId)?.PhoneNumber,
                UserName = users.Data.FirstOrDefault(b=>b.SubjectId.ToLower()==a.UserId)?.UserName,
            });
            return new GetAllOrderQueryResponse(true, orderModel);
        }
    }
}
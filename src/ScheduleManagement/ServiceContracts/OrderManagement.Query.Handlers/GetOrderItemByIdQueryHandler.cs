using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.QueryCommands.CartCheckoutQueryCommands.IntegrationEventQueryCommands;
using Common.Exceptions;
using Framework.Buses;
using Framework.Exception.Exceptions.Enum;
using Framework.Queries;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Persistence.EF.Context;
using OrderManagement.Query.Commands;

namespace OrderManagement.Query.Handlers
{
    public class GetOrderItemByIdQueryHandler:IQueryHandlerMediatR<GetOrderItemByIdQueryRequest,GetOrderItemByIdQueryResponse>
    {
        private readonly OrderManagementDbContext _context;
        private readonly IEventBus _eventBus;

        public GetOrderItemByIdQueryHandler(OrderManagementDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task<GetOrderItemByIdQueryResponse> Handle(GetOrderItemByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(a => a.OrderItems)
                .FirstOrDefaultAsync(a => a.Id == request.OrderId, cancellationToken: cancellationToken);
            if (order is null)
                throw new AppException(ResultCode.BadRequest, "can't find order");
            var orderItem = order.OrderItems.FirstOrDefault(a => a.Id == request.OrderItemId);
            if (orderItem is null)
                throw new AppException(ResultCode.BadRequest, "can't find order item");
            var cartCheckoutResponse=await _eventBus.IssueQuery(new IntegrateCalculatorCartCheckoutByCarCheckoutItemIdQueryRequest
            {
                CarCheckoutId = order.CartCheckoutId,
                ItemCartCheckoutId = orderItem.CartCheckoutItemId
            }, cancellationToken);
            var orderItemResponseQueryModel = new OrderItemByIdResponseQueryModel
            {
                OrderItemId = orderItem.Id,
                Company = orderItem.Company,
                Email = orderItem.Email,
                Price = orderItem.Price,
                AdditionalComment = orderItem.AdditionalComment,
                AdditionalInformation = orderItem.AdditionalInformation,
                BuyerNumber = orderItem.BuyerNumber,
                ContactName = orderItem.ContactName,
                CarrierName = cartCheckoutResponse.Data.CarrierName,
                OperableStatus = cartCheckoutResponse.Data.OperableStatus,
                PhoneNumber = orderItem.PhoneNumber,
                ReceiverInformation = orderItem.ReceiverInformation,
                StockNumber = orderItem.StockNumber,
                VinNumber = cartCheckoutResponse.Data.VinNumber,
                DestinationCountryName = cartCheckoutResponse.Data.DestinationCountryName,
                DestinationPortName = cartCheckoutResponse.Data.DestinationPortName,
                FullCarName = cartCheckoutResponse.Data.FullCarName,
                SourceCountryName = cartCheckoutResponse.Data.SourceCountryName,
                SourcePortName = cartCheckoutResponse.Data.SourcePortName,
                Address = cartCheckoutResponse.Data.Address
            };
            return new GetOrderItemByIdQueryResponse(true, orderItemResponseQueryModel);
        }
    }
}
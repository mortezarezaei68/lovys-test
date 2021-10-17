using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.QueryCommands.CalculatorQueryCommands;
using BasketManagement.QueryCommands.CartCheckoutQueryCommands.IntegrationEventQueryCommands;
using Common.Exceptions;
using Framework.Buses;
using Framework.Exception.Exceptions.Enum;
using Framework.Queries;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domains;
using OrderManagement.Persistence.EF.Context;
using OrderManagement.Query.Commands;

namespace OrderManagement.Query.Handlers
{
    public class GetOrderByIdQueryHandler : IQueryHandlerMediatR<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        private readonly OrderManagementDbContext _context;
        private readonly IEventBus _eventBus;
        public GetOrderByIdQueryHandler(OrderManagementDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(a => a.OrderItems)
                .FirstOrDefaultAsync(a => a.Id == request.OrderId, cancellationToken: cancellationToken);

            if (order is null)
                throw new AppException(ResultCode.BadRequest, "can't find order");
            var orderItemResponseQueryModels = new List<OrderItemResponseQueryModel>();
            foreach (var orderItem in order.OrderItems)
            {
                var cartCheckoutResponse=await _eventBus.IssueQuery(new IntegrateCalculatorCartCheckoutByCarCheckoutItemIdQueryRequest
                {
                    CarCheckoutId = order.CartCheckoutId,
                    ItemCartCheckoutId = orderItem.CartCheckoutItemId
                }, cancellationToken);
                orderItemResponseQueryModels.Add(new OrderItemResponseQueryModel
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
                });
            }

            var response = new OrderResponseQueryModel
            {
                Id = order.Id,
                Expired = order.IsExpired,
                TotalPrice = order.TotalPrice,
                TrackingCode = order.TrackingCode,
                OrderState = (int)order.OrderState,
                OrderItemResponseQueryModels = orderItemResponseQueryModels
            };
            return new GetOrderByIdQueryResponse(true, response);




        }
    }
}
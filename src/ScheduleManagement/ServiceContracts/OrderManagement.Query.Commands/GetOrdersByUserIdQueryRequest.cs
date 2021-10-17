using MediatR;

namespace OrderManagement.Query.Commands
{
    public class GetOrdersByUserIdQueryRequest:IRequest<GetOrdersByUserIdQueryResponse>
    {
    }
}
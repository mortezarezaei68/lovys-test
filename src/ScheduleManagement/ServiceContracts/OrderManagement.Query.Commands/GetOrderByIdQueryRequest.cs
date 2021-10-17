using MediatR;

namespace OrderManagement.Query.Commands
{
    public class GetOrderByIdQueryRequest : IRequest<GetOrderByIdQueryResponse>
    {
        public int OrderId { get; set; }
    }
}
using MediatR;

namespace OrderManagement.Query.Commands
{
    public class GetAllOrderQueryRequest:IRequest<GetAllOrderQueryResponse>
    {
    }
}
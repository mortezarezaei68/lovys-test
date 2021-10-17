using Framework.Queries;

namespace OrderManagement.Query.Commands
{
    public class GetOrderByIdQueryResponse : ResponseQuery<OrderResponseQueryModel>
    {
        public GetOrderByIdQueryResponse(bool isSuccess, OrderResponseQueryModel data, int count = 1,
            string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
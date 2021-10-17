using System.Collections.Generic;
using System.Linq;
using Framework.Queries;

namespace OrderManagement.Query.Commands
{
    public class GetOrdersByUserIdQueryResponse:ResponseQuery<IEnumerable<GetOrderByUserIdQueryResponseModel>>
    {
        public GetOrdersByUserIdQueryResponse(bool isSuccess, IEnumerable<GetOrderByUserIdQueryResponseModel> data, int count = 1, string message = null) : base(isSuccess, data, data.Count(), message)
        {
        }
    }
}
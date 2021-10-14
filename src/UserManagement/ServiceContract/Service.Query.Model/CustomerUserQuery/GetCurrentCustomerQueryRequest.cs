using Framework.Queries;
using MediatR;

namespace Service.Query.Model.CustomerUserQuery
{
    public class GetCurrentCustomerQueryRequest:IRequest<GetCurrentCustomerQueryResponse>
    {
    }

    public class GetCurrentCustomerQueryResponse:ResponseQuery<CurrentCustomerQueryModel>
    {
        public GetCurrentCustomerQueryResponse(bool isSuccess, CurrentCustomerQueryModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
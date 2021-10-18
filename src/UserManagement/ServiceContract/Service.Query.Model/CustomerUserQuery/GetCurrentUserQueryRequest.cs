using Framework.Queries;
using MediatR;
using Service.Query.Model.AdminUserQuery;

namespace Service.Query.Model.CustomerUserQuery
{
    public class GetCurrentUserQueryRequest:IRequest<GetCurrentUserQueryResponse>
    {
    }

    public class GetCurrentUserQueryResponse:ResponseQuery<AdminUserModel>
    {
        public GetCurrentUserQueryResponse(bool isSuccess, AdminUserModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
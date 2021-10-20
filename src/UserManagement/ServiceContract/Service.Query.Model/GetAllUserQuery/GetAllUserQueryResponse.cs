using System.Collections.Generic;
using Framework.Query;

namespace Service.Query.Model.GetAllUserQuery
{
    public class GetAllUserQueryResponse:ResponseQuery<IEnumerable<UserModel>>
    {
        public GetAllUserQueryResponse(bool isSuccess, IEnumerable<UserModel> data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
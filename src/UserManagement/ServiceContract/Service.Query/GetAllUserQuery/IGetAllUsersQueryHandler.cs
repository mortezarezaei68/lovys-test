using System.Threading;
using System.Threading.Tasks;
using Framework.Query;
using Service.Query.Model.GetAllUserQuery;

namespace Service.Query.GetAllUserQuery
{
    public interface IGetAllUsersQueryHandler:IQueryHandler
    {
        Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request,
            CancellationToken cancellationToken);
    }
}
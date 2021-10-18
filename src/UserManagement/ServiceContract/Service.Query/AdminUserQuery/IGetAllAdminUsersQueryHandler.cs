using System.Threading;
using System.Threading.Tasks;
using Framework.Query;
using Service.Query.Model.AdminUserQuery;

namespace Service.Query.AdminUserQuery
{
    public interface IGetAllAdminUsersQueryHandler:IQueryHandler
    {
        Task<GetAllAdminUserQueryResponse> Handle(GetAllAdminUserQueryRequest request,
            CancellationToken cancellationToken);
    }
}
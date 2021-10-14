using MediatR;

namespace Service.Query.Model.AdminUserQuery
{
    public class GetAllAdminUsersFromRoleQueryRequest:IRequest<GetAllAdminUsersFromRoleQueryResponse>
    {
        public string RoleName { get; set; }        
    }
}
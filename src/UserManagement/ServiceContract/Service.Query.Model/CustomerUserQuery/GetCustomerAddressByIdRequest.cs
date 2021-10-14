using MediatR;

namespace Service.Query.Model.CustomerUserQuery
{
    public class GetCustomerAddressByIdRequest:IRequest<GetCustomerAddressByIdResponse>
    {
        public int Id { get; set; }
    }
}
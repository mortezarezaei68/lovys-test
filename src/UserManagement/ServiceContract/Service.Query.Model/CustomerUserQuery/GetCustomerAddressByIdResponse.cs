using Framework.Queries;

namespace Service.Query.Model.CustomerUserQuery
{
    public class GetCustomerAddressByIdResponse:ResponseQuery<CustomerAddressQueryModel>
    {
        public GetCustomerAddressByIdResponse(bool isSuccess, CustomerAddressQueryModel data, int count = 1, string message = null) : base(isSuccess, data, count, message)
        {
        }
    }
}
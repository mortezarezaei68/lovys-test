using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Queries;

namespace OrderManagement.Query.Commands
{
    public class GetAllOrderQueryResponse:ResponseQuery<IEnumerable<GetAllAdminOrderQueryModel>>
    {
        public GetAllOrderQueryResponse(bool isSuccess, IEnumerable<GetAllAdminOrderQueryModel> data, int count = 1, string message = null) : base(isSuccess, data, data.Count(), message)
        {
        }
    }

    public class GetAllAdminOrderQueryModel
    {
        public int Id { get; set; }
        public int OrderState { get;  set; }
        public decimal TotalPrice { get;  set; }
        public bool DocumentMailingFee { get;  set; }
        public string TrackingCode { get;  set; }
        public DateTime Expired { get;  set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsExpired { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
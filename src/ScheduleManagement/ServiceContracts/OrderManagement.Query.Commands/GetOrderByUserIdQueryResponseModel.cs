using System;

namespace OrderManagement.Query.Commands
{
    public class GetOrderByUserIdQueryResponseModel
    {
        public int Id { get; set; }
        public int OrderState { get;  set; }
        public decimal TotalPrice { get;  set; }
        public bool DocumentMailingFee { get;  set; }
        public string TrackingCode { get;  set; }
        public DateTime Expired { get;  set; }
        public bool IsExpired { get; set; }
    }
}
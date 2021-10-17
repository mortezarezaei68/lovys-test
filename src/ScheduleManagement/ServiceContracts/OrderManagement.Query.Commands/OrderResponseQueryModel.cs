using System.Collections.Generic;

namespace OrderManagement.Query.Commands
{
    public class OrderResponseQueryModel
    {
        public int Id { get; set; }
        public string TrackingCode { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Expired { get; set; }
        public int OrderState { get; set; }
        public IEnumerable<OrderItemResponseQueryModel> OrderItemResponseQueryModels { get; set; }
    }
}
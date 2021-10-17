namespace OrderManagement.Query.Commands
{
    public class OrderItemResponseQueryModel
    {
        public int OrderItemId { get; set; }
        public string FullCarName { get; set; }
        public string VinNumber { get; set; }
        public string CarrierName { get; set; }
        public string Address { get; set; }
        public string DestinationCountryName { get; set; }
        public string DestinationPortName { get; set; }
        public string SourceCountryName { get; set; }
        public string SourcePortName { get; set; }
        public bool OperableStatus { get; set; }
        public decimal Price { get; set; }
        public string StockNumber { get; set; }
        public string BuyerNumber { get; set; }
        public string Company { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AdditionalInformation { get; set; }
        public string ReceiverInformation { get; set; }
        public string AdditionalComment { get; set; }
    }
}
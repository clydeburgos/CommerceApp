namespace Commerce.ProductManagement.Domain.Entities
{
    public class ProductLocation
    {
        public int ProductLocationID { get; set; }
        public int ProductID { get; set; }
        public int WarehouseID { get; set; }
        public int Quantity { get; set; }
        public string LocationCode { get; set; }
    }
}

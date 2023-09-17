namespace Commerce.WarehouseManagement.Domain.Entities
{
    public class Warehouse
    {
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
}

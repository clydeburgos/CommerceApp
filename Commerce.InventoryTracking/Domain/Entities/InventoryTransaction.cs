namespace Commerce.InventoryTracking
{
    public class InventoryTransaction
    {
        public int TransactionID { get; set; }
        public int ProductID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
    }
}

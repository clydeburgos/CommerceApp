namespace Commerce.InventoryTracking
{
    public interface IInventoryTrackingRepository
    {
        Task<int> AddInventoryTransactionAsync(InventoryTransaction transaction);
        Task<bool> UpdateInventoryTransactionAsync(InventoryTransaction transaction);
        Task<bool> DeleteInventoryTransactionAsync(int transactionId);
        Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync();
        Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsByProductAsync(int productId);
    }
}

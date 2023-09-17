using Commerce.Dapper;

namespace Commerce.InventoryTracking.Repository
{
    public class InventoryTrackingRepository : IInventoryTrackingRepository
    {
        private readonly IDapperBase dapper;
        public InventoryTrackingRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }
        public async Task<int> AddInventoryTransactionAsync(InventoryTransaction transaction)
        {
            string query = @"INSERT INTO InventoryTransaction (ProductID, TransactionDate, TransactionType, Quantity) 
                     VALUES (@ProductID, @TransactionDate, @TransactionType, @Quantity);
                     SELECT CAST(SCOPE_IDENTITY() as int)";
            var transactionId = await dapper.ExecuteScalarAsync<int>(query, transaction);
            return transactionId;
        }
        public async Task<bool> UpdateInventoryTransactionAsync(InventoryTransaction transaction)
        {
            string query = @"UPDATE InventoryTransaction 
                         SET ProductID = @ProductID,
                         TransactionDate = @TransactionDate,
                         TransactionType = @TransactionType,
                         Quantity = @Quantity
                     WHERE TransactionID = @TransactionID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, transaction);
            return affectedRows > 0;
        }
        public async Task<bool> DeleteInventoryTransactionAsync(int transactionId)
        {
            string query = "DELETE FROM InventoryTransaction WHERE TransactionID = @TransactionID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { TransactionID = transactionId });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync()
        {
            string query = "SELECT * FROM InventoryTransaction";
            var transactions = await dapper.QueryListAsync<InventoryTransaction>(query);
            return transactions;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsByProductAsync(int productId)
        {
            string query = "SELECT * FROM InventoryTransaction WHERE ProductID = @ProductId";
            var transactions = await dapper.QueryListAsync<InventoryTransaction>(query, new { ProductId = productId });
            return transactions;
        }
    }
}

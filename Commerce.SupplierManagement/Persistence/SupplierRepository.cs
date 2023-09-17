using Commerce.Dapper;
namespace Commerce.SupplierManagement
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IDapperBase dapper;
        public SupplierRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }

        public async Task<int> AddSupplierAsync(Supplier supplier)
        {
            string query = @"INSERT INTO Supplier (SupplierName, ContactName, ContactEmail) 
                     VALUES (@SupplierName, @ContactName, @ContactEmail);
                     SELECT CAST(SCOPE_IDENTITY() as int)";
            var supplierId = await dapper.ExecuteScalarAsync<int>(query, supplier);
            return supplierId;
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            string query = @"UPDATE Supplier 
                     SET SupplierName = @SupplierName, 
                         ContactName = @ContactName, 
                         ContactEmail = @ContactEmail 
                     WHERE SupplierID = @SupplierID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, supplier);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteSupplierAsync(int supplierId)
        {
            string query = "DELETE FROM Supplier WHERE SupplierID = @SupplierID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { SupplierID = supplierId });
            return affectedRows > 0;
        }
    }
}

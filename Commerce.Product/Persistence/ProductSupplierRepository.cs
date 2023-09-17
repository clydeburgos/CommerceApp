using Commerce.Dapper;
using Commerce.ProductManagement.Domain.Entities;

namespace Commerce.ProductManagement.Persistence
{

    public class ProductSupplierRepository : IProductSupplierRepository
    {
        private readonly IDapperBase dapper;
        public ProductSupplierRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }
        public async Task<bool> AddProductSupplierAsync(ProductSupplier productSupplier)
        {
            string query = @"INSERT INTO ProductSupplier (ProductID, SupplierID) 
                     VALUES (@ProductID, @SupplierID)";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, productSupplier);
            return affectedRows > 0;
        }

        public async Task<bool> RemoveProductSupplierAsync(int productId, int supplierId)
        {
            string query = "DELETE FROM ProductSupplier WHERE ProductID = @ProductID AND SupplierID = @SupplierID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { ProductID = productId, SupplierID = supplierId });
            return affectedRows > 0;
        }
    }
}

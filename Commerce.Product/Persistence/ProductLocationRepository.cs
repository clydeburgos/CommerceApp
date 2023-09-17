using Commerce.ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Dapper;

namespace Commerce.ProductManagement.Persistence
{
    public class ProductLocationRepository : IProductLocationRepository
    {
        private readonly IDapperBase dapper;
        public ProductLocationRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }

        public async Task<int> AddProductLocationAsync(ProductLocation productLocation)
        {
            string query = "INSERT INTO ProductLocation (ProductID, WarehouseID, Quantity, LocationCode) " +
                           "VALUES (@ProductID, @WarehouseID, @Quantity, @LocationCode);" +
                           "SELECT CAST(SCOPE_IDENTITY() as int)";
            return await dapper.ExecuteScalarAsync<int>(query, productLocation);
        }

        public async Task<bool> UpdateProductQtyInLocationAsync(int productLocationId, int quantity)
        {
            string query = "UPDATE ProductLocation SET Quantity = @Quantity WHERE ProductLocationID = @ProductLocationID";
            int rowsAffected = await dapper.ExecuteNonQueryAsync(query, new { ProductLocationID = productLocationId, Quantity = quantity });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteProductLocationAsync(int productLocationId)
        {
            string query = "DELETE FROM ProductLocation WHERE ProductLocationID = @ProductLocationID";
            int rowsAffected = await dapper.ExecuteNonQueryAsync(query, new { ProductLocationID = productLocationId });
            return rowsAffected > 0;
        }

        public async Task<ProductLocation> GetProductLocationAsync(int productLocationId)
        {
            string query = "SELECT * FROM ProductLocation WHERE ProductLocationID = @ProductLocationID";
            return await dapper.QuerySingleOrDefaultAsync<ProductLocation>(query, new { ProductLocationID = productLocationId });
        }

        public async Task<IEnumerable<ProductLocation>> GetProductLocationsByProductAsync(int productId)
        {
            string query = "SELECT * FROM ProductLocation WHERE ProductID = @ProductID";
            return await dapper.QueryListAsync<ProductLocation>(query, new { ProductID = productId });
        }

        public async Task<IEnumerable<ProductLocation>> GetProductLocationsByWarehouseAsync(int warehouseId)
        {
            string query = "SELECT * FROM ProductLocation WHERE WarehouseID = @WarehouseID";
            return await dapper.QueryListAsync<ProductLocation>(query, new { WarehouseID = warehouseId });
        }
    }
}

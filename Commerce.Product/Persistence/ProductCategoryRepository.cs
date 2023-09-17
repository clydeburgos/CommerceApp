using Commerce.Dapper;
using Commerce.ProductManagement.Domain.Entities;

namespace Commerce.ProductManagement.Persistence
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly IDapperBase dapper;
        public ProductCategoryRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }
        public async Task<int> AddProductCategoryAsync(ProductCategory productCategory)
        {
            string query = @"INSERT INTO ProductCategory (CategoryName) 
                     VALUES (@CategoryName);
                     SELECT CAST(SCOPE_IDENTITY() as int)";
            var categoryId = await dapper.ExecuteScalarAsync<int>(query, productCategory);
            return categoryId;
        }

        public async Task<bool> DeleteProductCategoryAsync(int categoryId)
        {
            string query = "DELETE FROM ProductCategory WHERE CategoryID = @CategoryId";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { CategoryId = categoryId });
            return affectedRows > 0;
        }

        public async Task<bool> UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            string query = @"UPDATE ProductCategory 
                     SET CategoryName = @CategoryName 
                     WHERE CategoryID = @CategoryID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, productCategory);
            return affectedRows > 0;
        }

        public async Task<bool> AddProductToCategoryAsync(ProductCategoryMapping productCategoryMapping)
        {
            string query = @"INSERT INTO ProductCategoryMapping (ProductID, CategoryID) 
                     VALUES (@ProductID, @CategoryID)";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, productCategoryMapping);
            return affectedRows > 0;
        }
        public async Task<bool> RemoveProductFromCategoryAsync(int productId, int categoryId)
        {
            string query = "DELETE FROM ProductCategoryMapping WHERE ProductID = @ProductID AND CategoryID = @CategoryID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { ProductID = productId, CategoryID = categoryId });
            return affectedRows > 0;
        }
    }
}

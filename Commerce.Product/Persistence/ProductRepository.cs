using Commerce.Dapper;
using Commerce.ProductManagement.Domain.Entities;
using Commerce.ProductManagement.Persistence;

namespace Commerce.ProductManagement
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDapperBase dapper;
        public ProductRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            string query = "SELECT * FROM Product";
            var products = await dapper.QueryListAsync<Product>(query);
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            string query = "SELECT * FROM Product WHERE ProductID = @ProductId";
            var product = await dapper.QueryFirstOrDefaultAsync<Product>(query, new { ProductId = productId });
            return product;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            string query = @"INSERT INTO Product (ProductName, Description, Price, Quantity, Category) 
                         VALUES (@ProductName, @Description, @Price, @Quantity, @Category);
                         SELECT CAST(SCOPE_IDENTITY() as int)";
            var productId = await dapper.ExecuteScalarAsync<int>(query, product);
            return productId;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            string query = @"UPDATE Product 
                         SET ProductName = @ProductName, 
                             Description = @Description, 
                             Price = @Price, 
                             Quantity = @Quantity, 
                             Category = @Category 
                         WHERE ProductID = @ProductID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, product);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            string query = "DELETE FROM Product WHERE ProductID = @ProductId";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { ProductId = productId });
            return affectedRows > 0;
        }
    }
}

using Commerce.ProductManagement.Domain.Entities;

namespace Commerce.ProductManagement.Persistence
{
    public interface IProductLocationRepository
    {
        Task<int> AddProductLocationAsync(ProductLocation product);
        Task<bool> UpdateProductQtyInLocationAsync(int productLocationId, int quantity);
        Task<bool> DeleteProductLocationAsync(int productLocationId);
        Task<ProductLocation> GetProductLocationAsync(int productLocationId);
        Task<IEnumerable<ProductLocation>> GetProductLocationsByProductAsync(int productId);
        Task<IEnumerable<ProductLocation>> GetProductLocationsByWarehouseAsync(int warehouseId);
    }
}

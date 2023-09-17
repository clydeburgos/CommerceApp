using Commerce.ProductManagement.Domain.Entities;

namespace Commerce.ProductManagement.Persistence
{
    public interface IProductCategoryRepository
    {
        Task<int> AddProductCategoryAsync(ProductCategory productCategory);
        Task<bool> UpdateProductCategoryAsync(ProductCategory productCategory);
        Task<bool> DeleteProductCategoryAsync(int categoryId);

        Task<bool> AddProductToCategoryAsync(ProductCategoryMapping productCategoryMapping);
        Task<bool> RemoveProductFromCategoryAsync(int productId, int categoryId);
    }
}

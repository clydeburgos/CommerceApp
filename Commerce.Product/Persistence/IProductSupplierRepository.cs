using Commerce.ProductManagement.Domain.Entities;

namespace Commerce.ProductManagement.Persistence
{
    public interface IProductSupplierRepository
    {
        Task<bool> AddProductSupplierAsync(ProductSupplier productSupplier);
        Task<bool> RemoveProductSupplierAsync(int productId, int supplierId);
    }
}

using Commerce.WarehouseManagement.Domain.Entities;

namespace Commerce.WarehouseManagement
{
    public interface IWarehouseRepository
    {
        Task AddWarehouseAsync(Warehouse warehouse);
        Task UpdateWarehouseAsync(Warehouse warehouse);
        Task DeleteWarehouseAsync(int warehouseID);
        Task<Warehouse> GetWarehouseAsync(int warehouseID);
        Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
    }
}

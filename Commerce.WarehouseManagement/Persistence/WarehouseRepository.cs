using Commerce.Dapper;
using Commerce.WarehouseManagement.Domain.Entities;

namespace Commerce.WarehouseManagement
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IDapperBase dapper;
        public WarehouseRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }
        public async Task AddWarehouseAsync(Warehouse warehouse)
        {
            string query = "INSERT INTO Warehouse (WarehouseName, Location, Capacity) VALUES (@WarehouseName, @Location, @Capacity)";
            await dapper.ExecuteNonQueryAsync(query, warehouse);
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse)
        {
            string query = "UPDATE Warehouse SET WarehouseName = @WarehouseName, Location = @Location, Capacity = @Capacity WHERE WarehouseID = @WarehouseID";
            await dapper.ExecuteNonQueryAsync(query, warehouse);
        }

        public async Task DeleteWarehouseAsync(int warehouseID)
        {
            string query = "DELETE FROM Warehouse WHERE WarehouseID = @WarehouseID";
            await dapper.ExecuteNonQueryAsync(query, new { WarehouseID = warehouseID });
        }

        public async Task<Warehouse> GetWarehouseAsync(int warehouseID)
        {
            string query = "SELECT * FROM Warehouse WHERE WarehouseID = @WarehouseID";
            return await dapper.QuerySingleOrDefaultAsync<Warehouse>(query, new { WarehouseID = warehouseID });
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
        {
            string query = "SELECT * FROM Warehouse";
            return await dapper.QueryListAsync<Warehouse>(query);
        }
    }
}
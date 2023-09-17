using Commerce.Dapper;

namespace Commerce.OrderManagement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDapperBase dapper;
        public CustomerRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            string query = @"INSERT INTO Customer (FirstName, LastName, Email, Phone) 
                     VALUES (@FirstName, @LastName, @Email, @Phone);
                     SELECT CAST(SCOPE_IDENTITY() as int)";
            var customerId = await dapper.ExecuteScalarAsync<int>(query, customer);
            return customerId;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { CustomerID = customerId });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            string query = "SELECT * FROM Customer";
            var customers = await dapper.QueryListAsync<Customer>(query);
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            string query = "SELECT * FROM Customer WHERE CustomerID = @CustomerId";
            var customer = await dapper.QueryFirstOrDefaultAsync<Customer>(query, new { CustomerId = customerId });
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            string query = @"UPDATE Customer 
                     SET FirstName = @FirstName,
                         LastName = @LastName,
                         Email = @Email,
                         Phone = @Phone
                     WHERE CustomerID = @CustomerID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, customer);
            return affectedRows > 0;
        }
    }
}

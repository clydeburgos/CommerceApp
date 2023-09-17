namespace Commerce.OrderManagement
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}

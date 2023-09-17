using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.OrderManagement.Domain.Entities;

namespace Commerce.OrderManagement
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(int customerId, DateTime orderDate, List<OrderItem> orderItems);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> CancelOrderAsync(int orderId);
        Task<Order> GetOrderDetailsAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
        Task<bool> AddOrderItemAsync(int orderId, int productId, int quantity);
        Task<bool> UpdateOrderItemAsync(int orderItemId, int updatedQuantity);
        Task<bool> RemoveOrderItemAsync(int orderItemId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
        Task<bool> InitiateReturnAsync(int orderId, string returnReason);
        Task<bool> ProcessRefundAsync(int orderId);
        Task<IEnumerable<Order>> SearchOrdersAsync(string criteria);
    }
}

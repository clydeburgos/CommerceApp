using System.Data.SqlClient;
using System.Data;
using Commerce.Dapper;
using Dapper;
using Commerce.OrderManagement.Domain.Entities;

namespace Commerce.OrderManagement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDapperBase dapper;
        public OrderRepository(IDapperBase dapper)
        {
            this.dapper = dapper;
        }

        public async Task<int> CreateOrderAsync(int customerId, DateTime orderDate, List<OrderItem> orderItems)
        {
            using IDbConnection dbConnection = new SqlConnection(dapper.ConnString);
            dbConnection.Open();

            using var transaction = dbConnection.BeginTransaction();

            try
            {
                // Create the order
                var orderQuery = "INSERT INTO OrderTable (CustomerID, OrderDate, TotalAmount, OrderStatus) " +
                                  "VALUES (@CustomerID, @OrderDate, @TotalAmount, @OrderStatus);" +
                                  "SELECT CAST(SCOPE_IDENTITY() as int)";
                var orderId = await dbConnection.ExecuteScalarAsync<int>(
                    orderQuery,
                    new
                    {
                        CustomerID = customerId,
                        OrderDate = orderDate,
                        TotalAmount = 0,
                        OrderStatus = "Pending"
                    },
                    transaction
                );

                // Add order items
                foreach (var orderItem in orderItems)
                {
                    var orderItemQuery = "INSERT INTO OrderItem (OrderID, ProductID, Quantity, Price) " +
                                         "VALUES (@OrderID, @ProductID, @Quantity, @Price)";
                    await dbConnection.ExecuteAsync(
                        orderItemQuery,
                        new
                        {
                            OrderID = orderId,
                            ProductID = orderItem.ProductID,
                            Quantity = orderItem.Quantity,
                            Price = orderItem.Price
                        },
                        transaction
                    );
                }

                // Commit the transaction
                transaction.Commit();

                return orderId;
            }
            catch
            {
                // Roll back the transaction in case of an exception
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            string query = "UPDATE OrderTable " +
                           "SET CustomerID = @CustomerID, OrderDate = @OrderDate, TotalAmount = @TotalAmount, OrderStatus = @OrderStatus " +
                           "WHERE OrderID = @OrderID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, order);
            return affectedRows > 0;
        }

        public async Task<bool> CancelOrderAsync(int orderId)
        {
            string query = "UPDATE OrderTable SET OrderStatus = 'Cancelled' WHERE OrderID = @OrderID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { OrderID = orderId });
            return affectedRows > 0;
        }

        public async Task<Order> GetOrderDetailsAsync(int orderId)
        {
            string query = "SELECT * FROM OrderTable WHERE OrderID = @OrderID";
            var order = await dapper.QueryFirstOrDefaultAsync<Order>(query, new { OrderID = orderId });
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            string query = "SELECT * FROM OrderTable";
            var orders = await dapper.QueryAsListAsync<Order>(query);
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            string query = "SELECT * FROM OrderTable WHERE CustomerID = @CustomerID";
            var orders = await dapper.QueryAsListAsync<Order>(query, new { CustomerID = customerId });
            return orders;
        }

        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            string query = "SELECT SUM(Quantity * Price) FROM OrderItem WHERE OrderID = @OrderID";
            var totalAmount = await dapper.ExecuteScalarAsync<decimal>(query, new { OrderID = orderId });
            return totalAmount;
        }

        public async Task<bool> AddOrderItemAsync(int orderId, int productId, int quantity)
        {
            string query = "INSERT INTO OrderItem (OrderID, ProductID, Quantity) VALUES (@OrderID, @ProductID, @Quantity)";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { OrderID = orderId, ProductID = productId, Quantity = quantity });
            return affectedRows > 0;
        }

        public async Task<bool> UpdateOrderItemAsync(int orderItemId, int updatedQuantity)
        {
            string query = "UPDATE OrderItem SET Quantity = @UpdatedQuantity WHERE OrderItemID = @OrderItemID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { OrderItemID = orderItemId, UpdatedQuantity = updatedQuantity });
            return affectedRows > 0;
        }

        public async Task<bool> RemoveOrderItemAsync(int orderItemId)
        {
            string query = "DELETE FROM OrderItem WHERE OrderItemID = @OrderItemID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { OrderItemID = orderItemId });
            return affectedRows > 0;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            string query = "UPDATE OrderTable SET OrderStatus = @NewStatus WHERE OrderID = @OrderID";
            var affectedRows = await dapper.ExecuteNonQueryAsync(query, new { OrderID = orderId, NewStatus = newStatus });
            return affectedRows > 0;
        }

        public async Task<bool> InitiateReturnAsync(int orderId, string returnReason)
        {
            var order = await GetOrderDetailsAsync(orderId);
            order.OrderStatus = "Returned";
            await UpdateOrderAsync(order);

            string insertReturnRecordQuery = "INSERT INTO ReturnRecord (OrderID, ReturnDate, ReturnReason) " +
                                              "VALUES (@OrderID, @ReturnDate, @ReturnReason)";

            await dapper.ExecuteNonQueryAsync(insertReturnRecordQuery,
                new
                {
                    OrderID = orderId,
                    ReturnDate = DateTime.Now,
                    ReturnReason = returnReason
                }
            );

            return true;
        }

        public async Task<bool> ProcessRefundAsync(int orderId)
        {
            var order = await GetOrderDetailsAsync(orderId);
            order.OrderStatus = "Refunded";
            await UpdateOrderAsync(order);

            string insertRefundRecordQuery = "INSERT INTO RefundRecord (OrderID, RefundDate, RefundAmount) " +
                                              "VALUES (@OrderID, @RefundDate, @RefundAmount)";

            // Get the order total amount
            decimal totalAmount = await CalculateOrderTotalAsync(orderId);
            await dapper.ExecuteNonQueryAsync(insertRefundRecordQuery,
                    new 
                    { 
                        OrderID = orderId, 
                        RefundDate = DateTime.Now, 
                        RefundAmount = totalAmount 
                    });
            return true;
        }

        public async Task<IEnumerable<Order>> SearchOrdersAsync(string criteria)
        {
            string query = "SELECT * FROM OrderTable WHERE ..."; // Implement criteria-based search query
            var orders = await dapper.QueryAsListAsync<Order>(query);
            return orders;
        }
    }
}

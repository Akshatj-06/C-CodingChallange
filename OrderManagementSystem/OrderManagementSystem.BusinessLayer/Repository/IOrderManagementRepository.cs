using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.Entity;

namespace OrderManagementSystem.BusinessLayer.Repository
{
    public interface IOrderManagementRepository
    {
        // Create an order for a user
        void CreateOrder(User user, List<Product> products);

        // Cancel an existing order
        void CancelOrder(int userId, int orderId);

        // Create a new product
        void CreateProduct(User user, Product product);

        // Create a new user
        void CreateUser(User user);

        // Get all products
        List<Product> GetAllProducts();

        // Get all orders by a specific user
        List<Order> GetOrdersByUser(User user);
    }
}

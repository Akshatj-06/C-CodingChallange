using System;
using System.Collections.Generic;
using OrderManagementSystem.Entity;
using OrderManagementSystem.BusinessLayer.Exceptions;
using OrderManagementSystem.BusinessLayer.Database;
using System.Data.SqlClient;

namespace OrderManagementSystem.BusinessLayer.Repository
{
    public class OrderProcessor : IOrderManagementRepository
    {
        public void CreateOrder(User user, List<Product> products)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                // Logic to create order in the database
            }
        }

        public void CancelOrder(int userId, int orderId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                // Logic to cancel the order
            }
        }

        public void CreateProduct(User user, Product product)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                // Logic to create product
            }
        }

        public void CreateUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                // Logic to create a user
            }
        }

        public List<Product> GetAllProducts()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                // Logic to get all products
                return new List<Product>();
            }
        }

        public List<Order> GetOrdersByUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                // Logic to get all orders by a user
                return new List<Order>();
            }
        }
    }
}

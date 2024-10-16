using OrderManagementSystem.BusinessLayer.Database;
using OrderManagementSystem.BusinessLayer.Exceptions;
using OrderManagementSystem.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OrderManagementSystem.BusinessLayer.Repository
{
    public class OrderManagementRepository : IOrderManagementRepository
    {
        // Create a new user and store it in the database
        public void CreateUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Create a new order for the user with the list of products
        public void CreateOrder(User user, List<Product> products)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                // Check if the user exists, if not create a new user
                if (!CheckUserExists(user.UserId))
                {
                    CreateUser(user);
                }

                foreach (var product in products)
                {
                    string orderQuery = "INSERT INTO Orders (UserId, ProductId, OrderDate, Quantity) VALUES (@UserId, @ProductId, @OrderDate, @Quantity)";
                    using (var command = new SqlCommand(orderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user.UserId);
                        command.Parameters.AddWithValue("@ProductId", product.ProductId);
                        command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Quantity", 1);  // Assuming 1 item for now
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        // Helper function to check if a user exists in the database
        private bool CheckUserExists(int userId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Cancel an order by checking if the order exists
        public void CancelOrder(int userId, int orderId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Orders WHERE OrderId = @OrderId AND UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@UserId", userId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        throw new OrderNotFoundException("Order not found.");
                    }

                    string deleteQuery = "DELETE FROM Orders WHERE OrderId = @OrderId AND UserId = @UserId";
                    using (var deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@OrderId", orderId);
                        deleteCommand.Parameters.AddWithValue("@UserId", userId);
                        deleteCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        // Create a product if the user is an admin
        public void CreateProduct(User user, Product product)
        {
            if (user.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Products (ProductId, ProductName, Description, Price, QuantityInStock, Type) " +
                               "VALUES (@ProductId, @ProductName, @Description, @Price, @QuantityInStock, @Type)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                    command.Parameters.AddWithValue("@Type", product.Type);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get all products from the database
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Products";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                ProductId = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDouble(3),
                                QuantityInStock = reader.GetInt32(4),
                                Type = reader.GetString(5)
                            };
                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }

        // Get all orders for a specific user
        public List<Order> GetOrdersByUser(User user)
        {
            var orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Orders WHERE UserId = @UserId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {
                                OrderId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                ProductId = reader.GetInt32(2),
                                OrderDate = reader.GetDateTime(3),
                                Quantity = reader.GetInt32(4)
                            };
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }
    }
}

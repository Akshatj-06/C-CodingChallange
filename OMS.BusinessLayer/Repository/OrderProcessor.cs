using OMS.Entities;
using OMS.BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using OMS.BusinessLayer.DatabaseConnection;

namespace OMS.BusinessLayer.Repository
{
    public class OrderProcessor : IOrderManagementRepository
    {
        // Method to create an order
        public void CreateOrder(User user, List<Product> products)
        {
            using (var connection = DBUtil.GetDBConn())
            {
                // Check if the user exists in the database
                var existingUser = GetUserById(connection, user.UserId);
                if (existingUser == null)
                {
                    // If not, create the user
                    CreateUser(user);

                }

                // Logic to store the order in the database
                var newOrder = new Order
                {
                    UserId = user.UserId,
                    OrderDate = DateTime.Now
                };

                // Insert the order into the database
                string insertOrderQuery = "INSERT INTO Orders (UserId, OrderDate) OUTPUT INSERTED.OrderId VALUES (@UserId, @OrderDate)";
                using (var command = new SqlCommand(insertOrderQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", newOrder.UserId);
                    command.Parameters.AddWithValue("@OrderDate", newOrder.OrderDate);
                    newOrder.OrderId = (int)command.ExecuteScalar();
                }

                // Link products to the order
                foreach (var product in products)
                {
                    AddProductToOrder(connection, newOrder.OrderId, product);
                }
            }
        }

        // Method to cancel an order
        public void CancelOrder(int userId, int orderId)
        {
            using (var connection = DBUtil.GetDBConn())
            {
                // Check if the userId exists in the database
                var existingUser = GetUserById(connection, userId);
                if (existingUser == null)
                {
                    throw new UserNotFoundException($"User with ID {userId} not found.");
                }

                // Check if the orderId exists in the database
                var existingOrder = GetOrderById(connection, orderId);
                if (existingOrder == null || existingOrder.UserId != userId)
                {
                    throw new OrderNotFoundException($"Order with ID {orderId} not found for user {userId}.");
                }

                // Logic to cancel the order
                string deleteOrderQuery = "DELETE FROM Orders WHERE OrderId = @OrderId";
                using (var command = new SqlCommand(deleteOrderQuery, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to create a product
        public void CreateProduct(User user, Product product)
        {
            using (var connection = DBUtil.GetDBConn())
            {
                // Check if the user is an admin
                if (user.Role != "Admin")
                {
                    throw new UnauthorizedAccessException("Only admin users can create products.");
                }

                // Logic to create and store the product in the database
                string insertProductQuery = "INSERT INTO Products (ProductName, Description, Price, QuantityInStock, Type) VALUES (@ProductName, @Description, @Price, @QuantityInStock, @Type)";
                using (var command = new SqlCommand(insertProductQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                    command.Parameters.AddWithValue("@Type", product.Type);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to create a user
        public void CreateUser(User user)
        {
            using (var connection = DBUtil.GetDBConn())
            {
                // Logic to create a user in the database
                string insertUserQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                using (var command = new SqlCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method to get all products
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (var connection = DBUtil.GetDBConn())
            {
                string selectProductsQuery = "SELECT * FROM Products";
                using (var command = new SqlCommand(selectProductsQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = (int)reader["ProductId"],
                                ProductName = reader["ProductName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (double)reader["Price"],
                                QuantityInStock = (int)reader["QuantityInStock"],
                                Type = reader["Type"].ToString()
                            });
                        }
                    }
                }
            }

            return products;
        }

        // Method to get orders by user
        public List<Product> GetOrderByUser(User user)
        {
            var orders = new List<Product>();

            using (var connection = DBUtil.GetDBConn())
            {
                string selectOrdersQuery = "SELECT p.* FROM Orders o JOIN OrderProducts op ON o.OrderId = op.OrderId JOIN Products p ON op.ProductId = p.ProductId WHERE o.UserId = @UserId";
                using (var command = new SqlCommand(selectOrdersQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Product
                            {
                                ProductId = (int)reader["ProductId"],
                                ProductName = reader["ProductName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (double)reader["Price"],
                                QuantityInStock = (int)reader["QuantityInStock"],
                                Type = reader["Type"].ToString()
                            });
                        }
                    }
                }
            }

            return orders;
        }

        // Helper method to get user by ID
        private User GetUserById(SqlConnection connection, int userId)
        {
            string selectUserQuery = "SELECT * FROM Users WHERE UserId = @UserId";
            using (var command = new SqlCommand(selectUserQuery, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserId = (int)reader["UserId"],
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
            }
            return null; // User not found
        }

        // Helper method to get order by ID
        private Order GetOrderById(SqlConnection connection, int orderId)
        {
            string selectOrderQuery = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            using (var command = new SqlCommand(selectOrderQuery, connection))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Order
                        {
                            OrderId = (int)reader["OrderId"],
                            UserId = (int)reader["UserId"],
                            OrderDate = (DateTime)reader["OrderDate"]
                        };
                    }
                }
            }
            return null; // Order not found
        }

        // Helper method to add product to order
        private void AddProductToOrder(SqlConnection connection, int orderId, Product product)
        {
            string insertProductToOrderQuery = "INSERT INTO OrderProducts (OrderId, ProductId) VALUES (@OrderId, @ProductId)";
            using (var command = new SqlCommand(insertProductToOrderQuery, connection))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.ExecuteNonQuery();
            }
        }
    }
}

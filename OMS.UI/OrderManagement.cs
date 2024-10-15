using OMS.Entities;
using OMS.BusinessLayer.Repository;
using System;
using System.Collections.Generic;

namespace OMS.UI
{
    class Program
    {
        private static IOrderManagementRepository _orderProcessor;

        static void Main(string[] args)
        {
            // Instantiate the OrderProcessor using DBUtil to get a database connection
            _orderProcessor = new OrderProcessor();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nOrder Management System");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Cancel Order");
                Console.WriteLine("4. Get All Products");
                Console.WriteLine("5. Get Orders by User");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateUser();
                        break;
                    case "2":
                        CreateProduct();
                        break;
                    case "3":
                        CancelOrder();
                        break;
                    case "4":
                        GetAllProducts();
                        break;
                    case "5":
                        GetOrdersByUser();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void CreateUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter role (Admin/User): ");
            string role = Console.ReadLine();

            User newUser = new User { Username = username, Password = password, Role = role };

            try
            {
                _orderProcessor.CreateUser(newUser);
                Console.WriteLine("User created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
            }
        }

        private static void CreateProduct()
        {
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();
            Console.Write("Enter description: ");
            string description = Console.ReadLine();
            Console.Write("Enter price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter quantity in stock: ");
            int quantityInStock = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter type (Electronics/Clothing): ");
            string type = Console.ReadLine();

            Product newProduct = new Product
            {
                ProductName = productName,
                Description = description,
                Price = price,
                QuantityInStock = quantityInStock,
                Type = type
            };

            Console.Write("Enter your username (Admin only): ");
            string adminUsername = Console.ReadLine();

            User adminUser = new User { Username = adminUsername, Role = "Admin" };

            try
            {
                _orderProcessor.CreateProduct(adminUser, newProduct);
                Console.WriteLine("Product created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
            }
        }

        private static void CancelOrder()
        {
            Console.Write("Enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());

            try
            {
                _orderProcessor.CancelOrder(userId, orderId);
                Console.WriteLine("Order canceled successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error canceling order: {ex.Message}");
            }
        }

        private static void GetAllProducts()
        {
            try
            {
                List<Product> products = _orderProcessor.GetAllProducts();
                Console.WriteLine("\nList of Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"- {product.ProductName} (ID: {product.ProductId}, Price: {product.Price})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products: {ex.Message}");
            }
        }

        private static void GetOrdersByUser()
        {
            Console.Write("Enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            User user = new User { UserId = userId }; // Assuming you have a UserId property in User class

            try
            {
                List<Product> orderedProducts = _orderProcessor.GetOrderByUser(user);
                Console.WriteLine("\nOrdered Products:");
                foreach (var product in orderedProducts)
                {
                    Console.WriteLine($"- {product.ProductName} (ID: {product.ProductId})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving orders for user: {ex.Message}");
            }
        }
    }
}

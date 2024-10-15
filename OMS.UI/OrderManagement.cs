using OMS.Entities;
using OMS.BusinessLayer.Repository;
using OMS.BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace OMS.UI
{
    class OrderManagement
    {
        private readonly IOrderManagementRepository _orderProcessor;

        public OrderManagement(IOrderManagementRepository orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }

        static void Main(string[] args)
        {
            // Instantiate the OrderProcessor (assuming dependency injection)
            var orderManagement = new OrderManagement(new OrderProcessor(new YourDbContext()));

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
                        orderManagement.CreateUser();
                        break;
                    case "2":
                        orderManagement.CreateProduct();
                        break;
                    case "3":
                        orderManagement.CancelOrder();
                        break;
                    case "4":
                        orderManagement.GetAllProducts();
                        break;
                    case "5":
                        orderManagement.GetOrdersByUser();
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

        private void CreateUser()
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

        private void CreateProduct()
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

            // Fetch the admin user (assuming you have a method to retrieve user)
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

        private void CancelOrder()
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

        private void GetAllProducts()
        {
            List<Product> products = _orderProcessor.GetAllProducts();
            Console.WriteLine("\nList of Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"- {product.ProductName} (ID: {product.ProductId}, Price: {product.Price})");
            }
        }

        private void GetOrdersByUser()
        {
            Console.Write("Enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            User user = new User { UserId = userId }; // Assuming you have a UserId property in User class

            List<Product> orderedProducts = _orderProcessor.GetOrderByUser(user);
            Console.WriteLine("\nOrdered Products:");
            foreach (var product in orderedProducts)
            {
                Console.WriteLine($"- {product.ProductName} (ID: {product.ProductId})");
            }
        }
    }
}

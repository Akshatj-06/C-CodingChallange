using System;
using System.Collections.Generic;
using OrderManagementSystem.BusinessLayer.Repository;
using OrderManagementSystem.Entity;

namespace OrderManagementSystem.BusinessLayer
{
    public class OrderManagement
    {
        private readonly OrderProcessor _orderProcessor;

        public OrderManagement()
        {
            _orderProcessor = new OrderProcessor(); // You can inject this dependency if needed
        }

        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n*** Order Management System ***");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Cancel Order");
                Console.WriteLine("4. Get All Products");
                Console.WriteLine("5. Get Orders by User");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

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
                        Console.WriteLine("Exiting the system...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateUser()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter role (Admin/User):");
            string role = Console.ReadLine();

            User user = new User { Username = username, Password = password, Role = role };
            _orderProcessor.CreateUser(user);

            Console.WriteLine("User created successfully!");
        }

        private void CreateProduct()
        {
            Console.WriteLine("Enter admin username:");
            string adminUsername = Console.ReadLine();

            // You can add more validation for admin role if needed
            Console.WriteLine("Enter product name:");
            string productName = Console.ReadLine();

            Console.WriteLine("Enter description:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter price:");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter quantity in stock:");
            int quantityInStock = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter product type (Clothing/Electronics):");
            string type = Console.ReadLine();

            User admin = new User { Username = adminUsername, Role = "Admin" };
            Product product = new Product
            {
                ProductName = productName,
                Description = description,
                Price = price,
                QuantityInStock = quantityInStock,
                Type = type
            };

            _orderProcessor.CreateProduct(admin, product);

            Console.WriteLine("Product created successfully!");
        }

        private void CancelOrder()
        {
            Console.WriteLine("Enter user ID:");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter order ID:");
            int orderId = Convert.ToInt32(Console.ReadLine());

            _orderProcessor.CancelOrder(userId, orderId);

            Console.WriteLine("Order cancelled successfully!");
        }

        private void GetAllProducts()
        {
            List<Product> products = _orderProcessor.GetAllProducts();
            Console.WriteLine("\nAll Products:");

            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.Price}, Stock: {product.QuantityInStock}");
            }
        }

        private void GetOrdersByUser()
        {
            Console.WriteLine("Enter user ID:");
            int userId = Convert.ToInt32(Console.ReadLine());

            User user = new User { UserId = userId };
            List<Order> orders = _orderProcessor.GetOrdersByUser(user);

            Console.WriteLine("\nOrders by User:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Product ID: {order.ProductId}, Quantity: {order.Quantity}, Order Date: {order.OrderDate}");
            }
        }
    }
}

using OrderManagementSystem.BusinessLayer;
using System;

namespace OrderManagementSystem.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the OrderManagement class
            OrderManagement orderManagement = new OrderManagement();
            // Show the menu and allow user interaction
            orderManagement.ShowMenu();

            // Prevent the console from closing immediately
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

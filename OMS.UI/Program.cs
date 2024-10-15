using OMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a Clothing object
            Clothing clothingProduct = new Clothing(2, "Shirt", "A classis shirt", 19.99, 100, "Clothing", "L", "Blue");

            // Print details of the clothing product
            Console.WriteLine(clothingProduct.ToString());

            // Create an Electronics object
            Electronics electronicProduct = new Electronics(1, "Tablet", "Biggest Screen Ever", 699.99, 50, "Electronics", "Samsung", 24);

            // Print details of the electronics product
            Console.WriteLine(electronicProduct.ToString());

            // Create a User object
            User adminUser = new User(1, "admin", "123", "Admin");

            // Print details of the user
            Console.WriteLine(adminUser.ToString());

            User regularUser = new User(2, "Aks", "123", "User");
            Console.WriteLine(regularUser.ToString());
            Console.ReadLine();
        }
    }
}

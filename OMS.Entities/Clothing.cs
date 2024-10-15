using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class Clothing : Product
    {
        public string Size { get; set; }
        public string Color { get; set; }

        // Default constructor
        public Clothing()
        {
        }

        // Parameterized constructor
        public Clothing(int productId, string productName, string description, double price, int quantityInStock, string type, string size, string color)
            : base()  // Call the base class constructor
        {
            // Initialize Product properties
            ProductId = productId;
            ProductName = productName;
            Description = description;
            Price = price;
            QuantityInStock = quantityInStock;
            Type = type;

            // Initialize Clothing-specific properties
            Size = size;
            Color = color;
        }

        // Override the ToString method to include the clothing-specific attributes
        public override string ToString()
        {
            return base.ToString() + $", Size: {Size}, Color: {Color}";
        }
    }
}

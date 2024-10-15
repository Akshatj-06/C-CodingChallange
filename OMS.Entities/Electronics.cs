using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class Electronics : Product
    {
        public string Brand { get; set; }
        public int WarrantyPeriod { get; set; }

        // Default constructor
        public Electronics()
        {
        }

        // Parameterized constructor
        public Electronics(int productId, string productName, string description, double price, int quantityInStock, string type, string brand, int warrantyPeriod)
            : base()  // Call the base class constructor
        {
            // Initialize Product properties
            ProductId = productId;
            ProductName = productName;
            Description = description;
            Price = price;
            QuantityInStock = quantityInStock;
            Type = type;

            // Initialize Electronics-specific properties
            Brand = brand;
            WarrantyPeriod = warrantyPeriod;
        }

        // Override the ToString method to include the electronics-specific attributes
        public override string ToString()
        {
            return base.ToString() + $", Brand: {Brand}, WarrantyPeriod: {WarrantyPeriod} months";
        }
    }
}

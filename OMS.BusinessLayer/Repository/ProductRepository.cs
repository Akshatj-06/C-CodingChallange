using OMS.Entities;
using OMS.BusinessLayer.Repository;

namespace OMS.BusinessLayer
{
    public class ProductRepository : IproductRepository
    {
        public Product Product { get; set; }

        // Default constructor
        public ProductRepository()
        {
            Product = new Product();
        }

        // Parameterized constructor
        public ProductRepository(int productId, string productName, string description, double price, int quantityInStock, string type)
        {
            Product = new Product
            {
                ProductId = productId,
                ProductName = productName,
                Description = description,
                Price = price,
                QuantityInStock = quantityInStock,
                Type = type
            };
        }

        // Override ToString method
        public override string ToString()
        {
            return $"ProductId: {Product.ProductId}, ProductName: {Product.ProductName}, Description: {Product.Description}, Price: {Product.Price}, QuantityInStock: {Product.QuantityInStock}, Type: {Product.Type}";
        }
    }
}

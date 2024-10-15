using OMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BusinessLayer.Repository
{
    public interface IOrderManagementRepository
    {
        void CreateOrder(User user, List<Product> products);
        void CancelOrder(int userId, int orderId);
        void CreateProduct(User user, Product product);
        void CreateUser(User user);
        List<Product> GetAllProducts();
        List<Product> GetOrderByUser(User user);
    }
}

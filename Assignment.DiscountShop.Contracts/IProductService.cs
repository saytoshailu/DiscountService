using System.Collections.Generic;
using Assignment.DiscountShop.Models;

namespace Assignment.DiscountShop.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        Product CreateProduct(string name, string description, decimal costPerUnit);
        void DeactivateProduct(Product prod);
        void ActivateProduct(Product prod);
    }
}
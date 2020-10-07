using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Assignment.DiscountShop.Contracts;
using Assignment.DiscountShop.Models;

namespace Assignment.DiscountShop.DiscountShopService
{
    public class CustomerService:ICustomerService
    {
        readonly List<Customer> _customers = new List<Customer>();
        private readonly IShoppingCartService _shoppingCartService;
        public CustomerService(IShoppingCartService scs)
        {
            _shoppingCartService = scs;
        }

        public int CreateCustomer(string name)
        {
            int maxId = _customers.Select(c => c.Id)
                .DefaultIfEmpty(0).Max();
            _customers.Add(new Customer(++maxId, name));
            return maxId;
        }
        public ShoppingCart CreateShoppingCart(int customerId)
        {
            var customer = _customers.Where(c => c.Id == customerId).Select(c => c).Single();
            
            if (customer == null)
                throw new InvalidDataException("Customer doesnt exist");

            return _shoppingCartService.CreateShoppingCart(0, customerId);
        }

        public ShoppingCart Checkout()
        {
            throw new NotImplementedException();
        }

        public ShoppingCart ComputeBill()
        {
            throw new NotImplementedException();
        }
    }
}

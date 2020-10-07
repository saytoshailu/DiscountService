using Assignment.DiscountShop.Models;

namespace Assignment.DiscountShop.Contracts
{
    public interface ICustomerService
    {
        int CreateCustomer(string name);
        ShoppingCart CreateShoppingCart(int customerId);
        ShoppingCart Checkout();
        ShoppingCart ComputeBill();
    }
}
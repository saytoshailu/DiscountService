using System.Collections.Generic;
using Assignment.DiscountShop.Models;

namespace Assignment.DiscountShop.Contracts
{
    public interface IDiscountService
    {
        IEnumerable<Discount> GetAll();
        Discount Get(int id);
        Discount CreateDiscount(string name, string description);
        DiscountCombinationItems CreateDiscountCombinationItems(int discountId, 
            DiscountCombinationItems discountCombinationItems);
        Discount UpdateDiscountCombinationItems(int discountId, 
            DiscountCombinationItems dci);
        void DeactivateDiscount(Discount discount);
        void ActivateDiscount(Discount discount);
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Assignment.DiscountShop.Contracts;
using Assignment.DiscountShop.Models;

namespace Assignment.DiscountShop.DiscountShopService
{
    public class DiscountService : IDiscountService
    {
        private readonly List<Discount> _discounts = new List<Discount>();

        public IEnumerable<Discount> GetAll()
        {
            return _discounts;
        }

        public Discount Get(int id)
        {
            return _discounts[id];
        }

        public Discount CreateDiscount(string name, string description)
        {
            var maxId = _discounts.Select(c => c.Id)
                .DefaultIfEmpty(0).Max();

            _discounts.Add(new Discount(++maxId, name, description));
            return _discounts[--maxId];
        }

        public DiscountCombinationItems CreateDiscountCombinationItems(int discountId,
            DiscountCombinationItems dci)
        {
            Discount discount = _discounts.Find(d => d.Id == discountId);

            if (discount==null) throw   new InvalidDataException("discountid does not exist");

            discount.UpdateDiscountCombinationItems(dci);
            return dci;
        }

        public Discount UpdateDiscountCombinationItems(int discountId,
            DiscountCombinationItems dci)
        {
            Discount discount = _discounts.Find(d => d.Id == discountId);

            if (discount == null) throw new InvalidDataException("discountid does not exist");
            
            discount.UpdateDiscountCombinationItems(dci);
            return _discounts[discountId];
        }


        public void DeactivateDiscount(Discount discount)
        {   
            discount.DeactivateProduct();
        }

        public void ActivateDiscount(Discount discount)
        {
            discount.ActivateProduct();
        }
    }
}
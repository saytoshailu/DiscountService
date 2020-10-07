using System.Collections.Generic;
using System.IO;

namespace Assignment.DiscountShop.Models
{
    public class DiscountCombinationItems
    {
        public DiscountCombinationItems(IEnumerable<KeyValuePair<Product, int>> itemsCountsCombinations,
            decimal discountAmount, decimal discountPercentage)
        {
            if (discountAmount > 0 && discountPercentage > 0 || discountAmount == 0 && discountPercentage == 0 ||
                discountAmount < 0 || discountPercentage < 0)
                throw new InvalidDataException(
                    "Incorrect amounts for discount, both discount amount and percentage can not be greater than zero or niether can be less than zero");

            ItemsCountsCombinations = itemsCountsCombinations;
            DiscountAmount = discountAmount;
            DiscountPercentage = discountPercentage;
        }

        public IEnumerable<KeyValuePair<Product, int>> ItemsCountsCombinations { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
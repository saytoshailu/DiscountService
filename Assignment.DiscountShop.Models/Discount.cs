namespace Assignment.DiscountShop.Models
{
    public class Discount
    {
        public Discount(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = DiscountStatus.Active;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public DiscountStatus Status { get; private set; }
        public DiscountCombinationItems DiscountCombinationItems { get; private set; }

        public void DeactivateProduct()
        {
            Status = DiscountStatus.Inactive;
        }

        public void ActivateProduct()
        {
            Status = DiscountStatus.Active;
        }

        public Discount UpdateDiscountCombinationItems(DiscountCombinationItems discountCombinationItems)
        {
            this.DiscountCombinationItems = discountCombinationItems;
            return this;
        }
    }
}
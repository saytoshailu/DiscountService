namespace Assignment.DiscountShop.Models
{
    public class Product
    {
        public Product(int id, string name, string description, decimal costPerUnit)
        {
            Id = id;
            Name = name;
            Description = description;
            CostPerUnit = costPerUnit;
            Status = ProductStatus.Active;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal CostPerUnit { get; }
        public ProductStatus Status { get; private set; }

        public void DeactivateDiscount()
        {
            Status = ProductStatus.Inactive;
        }
    }
}
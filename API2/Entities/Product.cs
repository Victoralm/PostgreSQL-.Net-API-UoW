namespace API2.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Category { get; set; }

        public Category ProductCategory { get; set; }
    }
}

namespace TESTPROJECT.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}

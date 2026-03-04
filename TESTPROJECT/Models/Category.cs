namespace TESTPROJECT.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Product> products = new List<Product>();
    }
}

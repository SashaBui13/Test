namespace TESTPROJECT.Models
{
    public class ProductToLocation
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsDeleted { get; set; }
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();
    }
}

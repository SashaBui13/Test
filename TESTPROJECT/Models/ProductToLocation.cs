namespace TESTPROJECT.Models
{
    public class ProductToLocation
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Product Product { get; set; }
        public Location Location { get; set; } 
    }
}

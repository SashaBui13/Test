namespace TESTPROJECT.Models
{
    public class ProductToLocation
    {
           public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsDeleted { get; set; }

    }
}

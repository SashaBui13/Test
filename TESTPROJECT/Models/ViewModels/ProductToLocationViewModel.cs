namespace TESTPROJECT.Models.ViewModels
{
    public class ProductToLocationViewModel
    {
        public List<ProductToLocation> productToLocations { get; set; }
        public ProductToLocation ProductToLocation { get; set; }
        public List<Location> Locations { get; set; } = new List<Location>();
        public List<Product> Products { get; set;} = new List<Product>();
        public Product selectProduct { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

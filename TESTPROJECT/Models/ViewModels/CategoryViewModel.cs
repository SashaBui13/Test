namespace TESTPROJECT.Models.ViewModels
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public string categoryDescription { get; set; }
    }
}

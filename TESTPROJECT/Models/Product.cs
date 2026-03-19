using System.ComponentModel.DataAnnotations;
namespace TESTPROJECT.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Назва не може бути такою короткою"), MaxLength(40, ErrorMessage = "Назва не може бути такою довгою")]
        public string Name { get; set; }
        [MinLength(20, ErrorMessage = "Опис занадто короткий"), MaxLength(250,  ErrorMessage = "Опис занадто довгий")]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Ціна має бути більше 0")]
        public int Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ImageUrl { get; set; }
        [MinLength(50, ErrorMessage = "Опис занадто короткий"), MaxLength(600, ErrorMessage = "Опис занадто довгий")]
        public string LongDescription { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductToLocation> ProductToLocations { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Product()
        {
            Comments = new List<Comment>();
        }

    }
}

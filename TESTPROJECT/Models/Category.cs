using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TESTPROJECT.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Назва категорії занадто коротка"), MaxLength(40, ErrorMessage = "Назва категорії занадто довга")] 

        public string Name { get; set; }
        [MinLength(5, ErrorMessage = "Опис категорії занадто короткий"), MaxLength(150, ErrorMessage = "Опис категорії занадто довгий")]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Product> products = new List<Product>();
    }
}

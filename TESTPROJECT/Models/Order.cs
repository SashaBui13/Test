using System.ComponentModel.DataAnnotations;

namespace TESTPROJECT.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";

        [Required(ErrorMessage = "Введіть прізвище")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введіть ім'я")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введіть номер телефону")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Формат: +380XXXXXXXXX")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Оберіть місто")]
        public string City { get; set; }

        [Required(ErrorMessage = "Оберіть відділення")]
        public string Warehouse { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
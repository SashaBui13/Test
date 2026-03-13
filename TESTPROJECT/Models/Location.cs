using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TESTPROJECT.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        [MinLength(3, ErrorMessage = "Назва локації занадто коротка"), MaxLength(40, ErrorMessage = "Назва локації занадто довга")]
        public string LocationName { get; set; }
        public string LocationAdress { get; set; }
        public string LocationMapsPath { get; set; }
        public bool LocationIsDeleted { get; set; }
        public List<ProductToLocation> ProductToLocations { get; set; }
    }
}

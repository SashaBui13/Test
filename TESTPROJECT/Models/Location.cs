using Microsoft.EntityFrameworkCore;

namespace TESTPROJECT.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAdress { get; set; }
        public string LocationMapsPath { get; set; }
        public bool LocationIsDeleted { get; set; }
        public List<ProductToLocation> ProductToLocations { get; set; }
    }
}

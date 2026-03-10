
namespace TESTPROJECT.Models.ViewModels
{
    public class LocationViewModel
    {
        public List<Location> Location { get; set; } = new List<Location>();
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAdress { get; set; }
        public string LocationMapsPath { get; set; }
        public bool LocationIsDeleted { get; set; }
    }
}

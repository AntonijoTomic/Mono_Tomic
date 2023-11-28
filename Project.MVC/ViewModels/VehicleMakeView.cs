using Project.Service.Classes;
using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeView
    {
        public SortingInfo? sort { get; set; }
        public PaginationInfo? PaginationInfo { get; set; }
        public IEnumerable<VehicleMake>? Makes { get; set; }

        public VehicleMake? make { get; set; } 
    }
}

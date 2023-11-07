using Project.Service.Classes;
using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleModelView
    {
        public VehicleModel? modell { get; set; } //
        public int? Id { get; set; }

        public string ?SortBy { get; set; }
        public string ?SortOrder { get; set; }
        public PaginationInfo ? PaginationInfo { get; set; }
        public int? filter { get; set; }

        public IEnumerable<VehicleMake> ? Makes { get; set; }
        public IEnumerable<VMVehicle>? models { get; set; }
    }
}

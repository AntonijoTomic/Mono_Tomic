using Project.Service.Classes;
using Project.Service.Models;

namespace Project.MVC.ViewModels
{
    public class VehicleModelView
    {
        public VehicleModel? Modell { get; set; } //
        public int? Id { get; set; }
        public SortingInfo? Sort { get; set; }

        //  public string ?SortBy { get; set; }
        //  public string ?SortOrder { get; set; }
        public PaginationInfo ? PaginationInfo { get; set; }
        public Filter_Info? Filter { get; set; }

        public IEnumerable<VehicleMake> ? Makes { get; set; }
        public IEnumerable<VMVehicle>? Models { get; set; }
    }
}

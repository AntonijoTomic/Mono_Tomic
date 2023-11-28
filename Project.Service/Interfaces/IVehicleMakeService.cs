using Project.Service.Classes;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleMakeService
    {

        Task<IEnumerable<VehicleMake>> GetAllVehiclesMakes();
    //    Task <IEnumerable<VehicleModel>> GetAllModels();//
        Task<VehicleMake> getMake(int makeID);
        Task<int> CreateVehicleMake(VehicleMake vehicle);
     //   Task<int> CreateVehicleModel(VehicleModel vehicle);//
        Task<int> UpdateVehicleMake(VehicleMake vehicle);
        Task<int> DeleteVehicleMakes(int id);

      //  Task<int> UpdateVehicleModel(VehicleModel vehicle);//
     //   Task<int> DeleteVehicleModel(int id);//

        Task<(IEnumerable<VehicleMake>, int totalPages)> sortMakesAndFilter(SortingInfo sort, PagingInfo paging);
        //  Task<(IEnumerable<VehicleModel>, int totalPages)> SortModelsAndFilter(SortingInfo sort, PagingInfo paging);

        Task<IEnumerable<VehicleMake>> FilterBySearch(List<VehicleMake> makes, string search);

    //   Task<IEnumerable<VehicleModel>> FilterByMake(int filterId);//

      //  IQueryable<VehicleModel> SortModels(IQueryable<VehicleModel> models,SortingInfo sort);//

    
    }
}

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

        Task<IEnumerable<VehicleMake>> GetAllVehicleMakesAsync();
    //    Task <IEnumerable<VehicleModel>> GetAllModels();//
        Task<VehicleMake> GetMakeAsync(int makeID);
        Task<int> CreateVehicleMakeAsync(VehicleMake vehicle);
     //   Task<int> CreateVehicleModel(VehicleModel vehicle);//
        Task<int> UpdateVehicleMakeAsync(VehicleMake vehicle);
        Task<int> DeleteVehicleMakesAsync(int id);

      //  Task<int> UpdateVehicleModel(VehicleModel vehicle);//
     //   Task<int> DeleteVehicleModel(int id);//

        Task<PagedResult<VehicleMake>> SortMakesAndFilterAsync(SortingInfo sort, PagingInfo paging, Filter_Info filter);
        //  Task<(IEnumerable<VehicleModel>, int totalPages)> SortModelsAndFilter(SortingInfo sort, PagingInfo paging);

       // Task<IEnumerable<VehicleMake>> FilterBySearch(List<VehicleMake> makes, string search);

    //   Task<IEnumerable<VehicleModel>> FilterByMake(int filterId);//

      //  IQueryable<VehicleModel> SortModels(IQueryable<VehicleModel> models,SortingInfo sort);//

    
    }
}

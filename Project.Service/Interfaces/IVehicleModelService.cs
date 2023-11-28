using Project.Service.Classes;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleModelService
    {
        Task<IEnumerable<VehicleModel>> GetAllModels();
        Task<VehicleModel> GetById(int id);
        Task<int> CreateVehicleModel(VehicleModel vehicle);
        Task<int> UpdateVehicleModel(VehicleModel vehicle);
        Task<int> DeleteVehicleModel(int id);
        Task<(IEnumerable<VehicleModel>, int totalPages)> SortModelsAndFilter(SortingInfo sort, PagingInfo paging);

        Task<IEnumerable<VehicleModel>> FilterByMake(int filterId);


    }
}

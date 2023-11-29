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
        Task<IEnumerable<VehicleModel>> GetAllModelsAsync();
        Task<VehicleModel> GetByIdAsync(int id);
        Task<int> CreateVehicleModelAsync(VehicleModel vehicle);
        Task<int> UpdateVehicleModelAsync(VehicleModel vehicle);
        Task<int> DeleteVehicleModelAsync(int id);
        Task<PagedResult<VehicleModel>> SortModelsAndFilterAsync(SortingInfo sort, PagingInfo paging, Filter_Info filter);

        Task<IEnumerable<VehicleModel>> FilterByMakeAsync(int filterId);


    }
}

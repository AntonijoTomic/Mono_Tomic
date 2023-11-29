using Microsoft.EntityFrameworkCore;
using Project.Service.Classes;
using Project.Service.Interfaces;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly ApplicationDbContext _dbContext;
        public VehicleModelService(ApplicationDbContext vehicleRepository) { 
            _dbContext = vehicleRepository;
        }
        public async Task<int> CreateVehicleModelAsync(VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }
            try
            {
                _dbContext.VehicleModel.Add(vehicle);
                await _dbContext.SaveChangesAsync();
                return vehicle.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> DeleteVehicleModelAsync(int id)
        {
            try
            {
                var result = await _dbContext.VehicleModel.FirstOrDefaultAsync(e => e.Id == id);
                if (result != null)
                {
                    _dbContext.VehicleModel.Remove(result);
                    await _dbContext.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public  async Task<IEnumerable<VehicleModel>> FilterByMakeAsync(int filterId)
        {
            return await _dbContext.VehicleModel
                                               .Where(m => m.MakeId == filterId)
                                               .ToListAsync();
        }

        public  async Task<IEnumerable<VehicleModel>> GetAllModelsAsync()
        {
            return await _dbContext.VehicleModel.Include(vm => vm.Make).ToListAsync();
        }

        public async Task<VehicleModel> GetByIdAsync(int id)
        {
           return await _dbContext.VehicleModel.Where(m => m.Id == id).Include(vm => vm.Make).FirstOrDefaultAsync();
        }

  

        public async Task<PagedResult<VehicleModel>> SortModelsAndFilterAsync(SortingInfo sort, PagingInfo paging, Filter_Info filter)
        {
            var models = _dbContext.VehicleModel.Include(m => m.Make).AsQueryable();

            switch (sort.SortBy)
            {
                case "Name":
                    models = sort.SortOrder == "asc" ? models.OrderBy(m => m.Name) : models.OrderByDescending(m => m.Name);
                    break;
                case "Abrv":
                    models = sort.SortOrder == "asc" ? models.OrderBy(m => m.Abrv) : models.OrderByDescending(m => m.Abrv);
                    break;
                case "Makes":
                    var makes = _dbContext.VehicleMake.AsQueryable();
                    models = sort.SortOrder == "asc" ? models.OrderBy(m => makes.FirstOrDefault(make => make.Id == m.MakeId).Name) : models.OrderByDescending(m => makes.FirstOrDefault(make => make.Id == m.MakeId).Name);
                    break;
            }


            if (Convert.ToInt32(filter.Filter) > 0)
            {
                models = models.Where(m => m.Make.Id == (Convert.ToInt32(filter.Filter)));
            }

            var totalItems =  models.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            models = models.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var pagedResult = new PagedResult<VehicleModel>
            {
                Data = models,
                TotalPages = totalPages
            };

            return pagedResult;
        }

        public async Task<int> UpdateVehicleModelAsync(VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }

            try
            {
                var existingModel = await _dbContext.VehicleModel.FindAsync(vehicle.Id);

                if (existingModel == null)
                {
                    return 0;
                }

                existingModel.Name = vehicle.Name;
                existingModel.Abrv = vehicle.Abrv;
        
               // existingModel.Make = vehicle.Make;
                existingModel.MakeId = vehicle.MakeId;
                _dbContext.VehicleModel.Update(existingModel);
                await _dbContext.SaveChangesAsync();

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}

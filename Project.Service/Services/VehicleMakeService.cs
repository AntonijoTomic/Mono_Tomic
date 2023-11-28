using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Service.Classes;
using Project.Service.Interfaces;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Project.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ApplicationDbContext _dbContext;
  

        public VehicleMakeService(ApplicationDbContext vehicleRepository)
        {
            _dbContext = vehicleRepository;         
    
        }
        public async Task<int> CreateVehicleMake(VehicleMake vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }
            try
            {
                _dbContext.VehicleMake.Add(vehicle);
                await _dbContext.SaveChangesAsync();
                return vehicle.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


 
        public async Task<int> DeleteVehicleMakes(int id)
        {           
            try
            {
                var result = await _dbContext.VehicleMake.FirstOrDefaultAsync(e => e.Id == id);
                if (result != null)
                {
                    _dbContext.VehicleMake.Remove(result);
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

        public async Task<IEnumerable<VehicleMake>> GetAllVehiclesMakes()
        {
            return await _dbContext.VehicleMake.ToListAsync();
        }

       

        public async Task<int> UpdateVehicleMake(VehicleMake vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "");
            }

            try
            {
                var existingMake = await _dbContext.VehicleMake.FindAsync(vehicle.Id);

                if (existingMake == null)
                {
                    return 0;
                }

                existingMake.Name = vehicle.Name;
                existingMake.Abrv = vehicle.Abrv;

                _dbContext.VehicleMake.Update(existingMake);
                await _dbContext.SaveChangesAsync();

                return 1;    
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }


        public async Task<(IEnumerable<VehicleMake>, int totalPages)> sortMakesAndFilter(SortingInfo sort, PagingInfo paging)
        {
            var makes =  _dbContext.VehicleMake.AsQueryable();
            switch (sort.SortBy)
            {
                case "Name":
                    makes = sort.SortOrder == "asc" ? makes.OrderBy(m => m.Name): makes.OrderByDescending(m => m.Name);
                    break;
                case "Abrv":
                    makes = sort.SortOrder == "asc" ? makes.OrderBy(m => m.Abrv) : makes.OrderByDescending(m => m.Abrv);
                    break;
            }
 


            if (!string.IsNullOrEmpty(sort.Filter))
            {
                makes = makes.Where(m => m.Name.ToUpper().Contains(sort.Filter.ToUpper()));
            }

            var totalItems = makes.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            makes = makes.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            return (await makes.ToListAsync(), totalPages);
        } 

        public async Task<IEnumerable<VehicleMake>> FilterBySearch(List<VehicleMake> makes,string search)
        {
            return from m in makes where m.Name.ToUpper().Contains(search.ToUpper()) select m;
           // return makes.Where(m => m.Name.ToUpper().Contains(search.ToUpper()));
        }

     

       
        public async Task<VehicleMake> getMake(int makeId)
        {   
            return await _dbContext.VehicleMake.Where(m => m.Id == makeId).FirstOrDefaultAsync();   
        }


        

      
    }
}

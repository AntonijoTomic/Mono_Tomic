using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Ninject.Infrastructure.Language;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Classes;
using Project.Service.Interfaces;
using Project.Service.Models;
using Project.Service.Services;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IPaginationService<VehicleModel> _pagination;
        private readonly IMapper _mapper;
   

        public VehicleModelController(IMapper mapper)
        {
            _vehicleService = Di.Create<IVehicleService>();
            _pagination = Di.Create<IPaginationService<VehicleModel>>();
            _mapper = mapper;

        }

        public async Task<IActionResult> Index(string sortby, string sortorder, int filter, int pageSize = 5, int page = 1)
        {
            var models = await _vehicleService.GetAllModels();
            var makes = await _vehicleService.GetAllVehiclesMakes();


            if (sortby== "Makes")
            {
                models = await _vehicleService.SortModels(sortby, sortorder, (List<VehicleMake>)makes);
            }
            else if(!string.IsNullOrEmpty(sortby))
            {
                models = await _vehicleService.SortModels(sortby, sortorder, (List<VehicleMake>)makes);
            }
            if (filter >0)
            {
                models = await _vehicleService.FilterByMake((List<VehicleModel>) models, filter);
            }
            var totalItems = models.Count();
            var totalPages = _pagination.GetTotalPages(totalItems, pageSize);
            var pagedModels = _pagination.GetPage(models, page, pageSize);

            var paginationInfo = new PaginationInfo
            {
                TotalItems = totalItems,
                ItemsPerPage = pageSize,
                CurrentPage = page,
                TotalPages = totalPages
            };

   
           var modelView = new VehicleModelView();
            {
                modelView.SortBy = sortby;
                modelView.filter = filter;
                modelView.SortOrder = sortorder;
                modelView.Makes = makes;
                modelView.models = _mapper.Map<IEnumerable<VMVehicle>>(pagedModels);
                modelView.PaginationInfo = paginationInfo;
            }

            foreach(VMVehicle mo in modelView.models)
            {
                mo.make = await _vehicleService.getMake(mo.MakeId);
            }
           
            return View(modelView);
        }
        public async Task<ActionResult> Create()
        {
           var viewModel = new VehicleModelView // podaci za padajuci izbornik 
            {
                Makes = await _vehicleService.GetAllVehiclesMakes()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Create(VehicleModelView model)
        {
            
           // model.Makes = await _vehicleService.GetAllVehiclesMakes();
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<VehicleModel>(model.modell);
                await _vehicleService.CreateVehicleModel(entity);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            int result = await _vehicleService.DeleteVehicleModel(id);

            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else if (result == 0)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(500, "Failed to delete vehicle model.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVehicleModel(VehicleModel updatedVehicle)
        {
            int result = await _vehicleService.UpdateVehicleModel(updatedVehicle);

            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else if (result == 0)
            {
                return NotFound("Vehicle model not found.");
            }
            else
            {
                return StatusCode(500, "Failed to update vehicle model.");
            }
        }
    }
}

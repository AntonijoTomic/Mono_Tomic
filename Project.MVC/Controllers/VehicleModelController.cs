using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        private readonly IVehicleMakeService _vehicleService;
        private readonly IVehicleModelService _vehicleModelService;

        private readonly IMapper _mapper;
   

        public VehicleModelController(IMapper mapper)
        {
            _vehicleService = Di.Create<IVehicleMakeService>();
            _vehicleModelService = Di.Create <IVehicleModelService>();

            _mapper = mapper;

        }

        public async Task<IActionResult> Index(SortingInfo sort, PagingInfo paging, Filter_Info filter_Info)
        {
  
            var pagedResult = await _vehicleModelService.SortModelsAndFilterAsync(sort, paging, filter_Info);
          
            var paginationInfo = new PaginationInfo {
                CurrentPage = paging.PageNumber,
                TotalPages = pagedResult.TotalPages
            };
          
            var modelView = new VehicleModelView {
                Sort = sort, 
                Filter = filter_Info,
                PaginationInfo = paginationInfo,
                Makes = await _vehicleService.GetAllVehicleMakesAsync(), 
                Models = _mapper.Map<IEnumerable<VMVehicle>>(pagedResult.Data) 
            };


            return View(modelView);
        }
        public async Task<ActionResult> Create()
        {
           var viewModel = new VehicleModelView // podaci za padajuci izbornik 
            {
                Makes = await _vehicleService.GetAllVehicleMakesAsync()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Create(VehicleModelView model)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<VehicleModel>(model.Modell);
                await _vehicleModelService.CreateVehicleModelAsync(entity);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            int result = await _vehicleModelService.DeleteVehicleModelAsync(id);

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
        public async Task<IActionResult> Edit(VehicleModelView modelView)           
        {        
            int result = await _vehicleModelService.UpdateVehicleModelAsync(modelView.Modell);

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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _vehicleModelService.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound(); 
            }

            var viewModel = new VehicleModelView
            {
                Modell = model,
                Makes = await _vehicleService.GetAllVehicleMakesAsync()
            };

            return View(viewModel);
        }

    }
}

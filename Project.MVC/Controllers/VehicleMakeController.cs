﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Classes;
using Project.Service.Interfaces;
using Project.Service.Models;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IMapper mapper)
        {
            _vehicleMakeService = Di.Create<IVehicleMakeService>();
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(SortingInfo sort, PagingInfo paging,  Filter_Info filter)
        {
          
            var pagedResult = await _vehicleMakeService.SortMakesAndFilterAsync(sort, paging, filter);
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = paging.PageNumber,
                TotalPages = pagedResult.TotalPages
            };

            var makesViewModel = new VehicleMakeView
            {
                Filter = filter,
                Sort = sort,
                PaginationInfo = paginationInfo,      
                Makes = _mapper.Map<IEnumerable<VehicleMake>>(pagedResult.Data)
            };

            return View(makesViewModel);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(VehicleMake make)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<VehicleMake>(make);

                await _vehicleMakeService.CreateVehicleMakeAsync(entity);
                return RedirectToAction("Index");
            }
            return View(make);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            int result = await _vehicleMakeService.DeleteVehicleMakesAsync(id);

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
                return StatusCode(500, "Failed to delete vehicle make.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(VehicleMakeView makeView)
        {
            int result = await _vehicleMakeService.UpdateVehicleMakeAsync(makeView.make);

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
            var make = await _vehicleMakeService.GetMakeAsync(id);

            if (make == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleMakeView
            {
                make = make

            };

            return View(viewModel);
        }
    }
}

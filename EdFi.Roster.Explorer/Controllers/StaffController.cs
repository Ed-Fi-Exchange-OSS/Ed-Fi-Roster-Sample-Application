﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffService _staffService;
        public StaffController(StaffService staffService)
        {
            _staffService = staffService; 
        }

        public async Task<IActionResult> Index()
        {
            var staffList = await _staffService.ReadAllAsync();
            return View(new ExtendedInfoResponse<List<Staff>>
            {
                FullDataSet = staffList.ToList(),
                IsExtendedInfoAvailable = false
            });
        }

        public async Task<IActionResult> LoadStaff()
        {
            var response = await _staffService.GetAllStaffWithExtendedInfoAsync();
            await _staffService.Save(response.FullDataSet);
            response.IsExtendedInfoAvailable = true;
            return View("Index", response);
        }
    }
}

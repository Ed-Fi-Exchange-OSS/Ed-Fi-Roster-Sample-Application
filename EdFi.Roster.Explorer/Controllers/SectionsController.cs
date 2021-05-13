﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class SectionsController : Controller
    {
        private readonly SectionService _sectionsService;

        public SectionsController(SectionService sectionService)
        {
            _sectionsService = sectionService;
        }
        public async Task<IActionResult> Index()
        {
            var sections = await _sectionsService.ReadAllAsync();
            return View(new ExtendedInfoResponse<List<Section>>
            {
                FullDataSet = sections.ToList(),
                IsExtendedInfoAvailable = false
            });
        }
        public async Task<IActionResult> LoadSections()
        {
            var response = await _sectionsService.GetAllSectionsWithExtendedInfoAsync();
            await _sectionsService.Save(response.FullDataSet);
            response.IsExtendedInfoAvailable = true;
            return View("Index", response);
        }
    }
}

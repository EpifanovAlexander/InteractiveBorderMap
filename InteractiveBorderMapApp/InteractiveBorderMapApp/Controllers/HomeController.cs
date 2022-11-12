using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using InteractiveBorderMapApp.Entities;
using InteractiveBorderMapApp.Models;
using InteractiveBorderMapApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InteractiveBorderMapApp.Controllers
{
    public class HomeController : Controller
    {
        private CoordinateService _coordinateService;
        private CriteriaService _criteriaService;
        private ILogger _logger;

        public HomeController(CoordinateService coordinateService, CriteriaService criteriaService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _coordinateService = coordinateService;
            _criteriaService = criteriaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Calculate()
        {
            using var reader = new StreamReader(Request.Body);
            var content = await reader.ReadToEndAsync();
            _logger.Log(LogLevel.Information, DateTime.Now + ": " + content);
            var coordinates = JsonSerializer.Deserialize<IEnumerable<Coordinate>>(content);

            IEnumerable<OsmBuilding> list = _coordinateService.getBuildingsAsync(coordinates).Result;

            var markers = new List<Marker>();
            foreach (var building in list)
            {
                markers.Add(new Marker(building.Coordinate, MarkerType.INCLUDE, building.Content));
            }

            var newContent = JsonSerializer.Serialize(markers);
            return newContent;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
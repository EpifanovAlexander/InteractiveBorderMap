using InteractiveBorderMapApp.Entities;
using InteractiveBorderMapApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using InteractiveBorderMapApp.Entities;
using InteractiveBorderMapApp.Models;
using InteractiveBorderMapApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveBorderMapApp.Controllers
{
    public class HomeController : Controller
    {
        private CoordinateService _coordinateService;
        private CriteriaService _criteriaService;
        
        public HomeController(CoordinateService coordinateService, CriteriaService criteriaService)
        {
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
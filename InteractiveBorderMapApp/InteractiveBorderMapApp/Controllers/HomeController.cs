using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InteractiveBorderMapApp.Entities;
using InteractiveBorderMapApp.Models;
using InteractiveBorderMapApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InteractiveBorderMapApp.Controllers
{
    public class HomeController : Controller
    {
        private CoordinateService _coordinateService;
        private CriteriaService _criteriaService;
        private ReportService _reportService;
        private ILogger _logger;
        private readonly IWebHostEnvironment _appEnvironment;

        public static string WebRootPath { get; private set; }

        public HomeController(CoordinateService coordinateService, CriteriaService criteriaService, 
            ReportService reportService, ILogger<HomeController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _coordinateService = coordinateService;
            _criteriaService = criteriaService;
            _reportService = reportService;
            _appEnvironment = appEnvironment;
            WebRootPath = _appEnvironment.WebRootPath;
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

            IEnumerable<Building> list = _coordinateService.getBuildingsAsync(coordinates).Result;

            var markers = new List<Marker>();
            foreach (var building in list)
            {
                if(!markers.Any(m => m.Coordinate.Lat == building.Center.Lat && m.Coordinate.Lng == building.Center.Lng))
                    markers.Add(new Marker(building.Center, building.Coordinates.ToArray(), MarkerType.INCLUDE, building.ToString()));
            }
            if (markers.Count > 2)
            {
                markers[0].MarkerType = MarkerType.EXCLUDE;
                markers[1].MarkerType = MarkerType.DISCUSS;
            }

            // Создаём лист со строениями для примера формирования отчёта
            //List<Building> buildings = new List<Building>();
            //Building build1 = new Building("11", "Пушкина", 1223, true, "1980", false, true, "Кирпичный");
            //Building build2 = new Building("22", "Ленина", 3221, false, "1981", true, true, "Монолитный");
            //Building build3 = new Building("33", "Есенина", 1133, true, "1982", false, true, "Панельный");
            //Building build4 = new Building("44", "Жукова", 2244, false, "1983", true, true, "Деревянный");

            //buildings.Add(build1);
            //buildings.Add(build2);
            //buildings.Add(build3);
            //buildings.Add(build4);

            var reportId = _reportService.CreateBuildReport(list.ToList());
            var responseModel = new ResponseModel(markers, reportId);

            var newContent = JsonSerializer.Serialize(responseModel);
            return newContent;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult DownloadReport(string id)
        {
            var fileStream = _reportService.GetReport(id);
            return File(fileStream, "application/octet-stream");
        }
    }
}
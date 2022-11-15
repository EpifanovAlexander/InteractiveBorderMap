using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            IEnumerable<OsmBuilding> list = _coordinateService.getBuildingsAsync(coordinates).Result;

            var markers = new List<Marker>();
            foreach (var building in list)
            {
                markers.Add(new Marker(building.Coordinate, MarkerType.INCLUDE, building.Content));
            }
            var reportId = "12.docx";
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
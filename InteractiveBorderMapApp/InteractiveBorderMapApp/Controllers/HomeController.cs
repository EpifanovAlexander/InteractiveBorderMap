﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public string Calculate()
        {
            using var reader = new StreamReader(Request.Body);
            var content = reader.ReadToEnd();
            var coordinates = JsonSerializer.Deserialize<IEnumerable<Coordinate>>(content);


            IEnumerable<Building> list = _coordinateService.getBuildingsAsync(coordinates).Result;


            // Новые координаты
            var newCoordinates = JsonSerializer.Deserialize<IEnumerable<Coordinate>>(content);
            newCoordinates.AsParallel().ForAll(i =>
            {
                i.Lat += 0.001;
                i.Lng -= 0.006;
            });
            var objects = new List<IEnumerable<Coordinate>>();
            objects.Add(newCoordinates);
            coordinates.AsParallel().ForAll(i =>
            {
                i.Lat += 0.004;
                i.Lng += 0.003;
            });
            objects.Add(coordinates);
            var newContent = JsonSerializer.Serialize(objects);
            return newContent;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
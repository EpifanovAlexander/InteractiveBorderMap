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

namespace InteractiveBorderMapApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

            // Если выводим через полигоны
            //var newCoordinates = JsonSerializer.Deserialize<IEnumerable<Coordinate>>(content);
            //newCoordinates.AsParallel().ForAll(i => { i.Lat += 0.001; i.Lng -= 0.006; });
            //var objects = new List<IEnumerable<Coordinate>>();
            //objects.Add(newCoordinates);
            //coordinates.AsParallel().ForAll(i => { i.Lat += 0.004; i.Lng += 0.003; });
            //objects.Add(coordinates);
            //var newContent = JsonSerializer.Serialize(objects);

            // Если выводим через маркеры
            var text = "Кадастровый номер: 1122<br>Рекомендация:снос-понос";
            var markers = new List<Marker>();
            double lat_sum = 0, lng_sum = 0;
            foreach (var coord in coordinates) 
            {
                lat_sum += coord.Lat;
                lng_sum += coord.Lng;
            }
            var center = new Coordinate() { Lat = lat_sum / coordinates.Count(), Lng = lng_sum / coordinates.Count() };
            markers.Add(new Marker() { Coordinate = center, MarkerType = MarkerType.INCLUDE, Text = text });
            markers.Add(new Marker() { Coordinate = new Coordinate() { Lat = center.Lat + 0.0009, Lng = center.Lng - 0.0008 }, MarkerType = MarkerType.EXCLUDE, Text = text });
            markers.Add(new Marker() { Coordinate = new Coordinate() { Lat = center.Lat - 0.0008, Lng = center.Lng + 0.0009 }, MarkerType = MarkerType.DISCUSS, Text = text });

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
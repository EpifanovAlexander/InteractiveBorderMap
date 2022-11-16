using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InteractiveBorderMapApp.CustomExceptions;
using InteractiveBorderMapApp.Entities;
using InteractiveBorderMapApp.Repositories;

namespace InteractiveBorderMapApp.Services
{
    public class CoordinateService
    {
        private const string OSM_URL = "https://api.openstreetmap.org/";
        private string _dbfPath = @"InteractiveBorderMapApp\Dataset\Организации СВАО_САО\Организации_СВАО_САО.dbf";


        private string _excelAreaPath =
            @"InteractiveBorderMapApp\Dataset\Аварийные_Самовольные_Несоответствие_ВРИ_СВАО_САО.XLSX";

        private IHttpClientFactory _clientFactory;
        private BuildingRepo _buildingRepo;

        public CoordinateService(IHttpClientFactory clientFactory, Parser parser, BuildingRepo buildingRepo)
        {
            _clientFactory = clientFactory;
            _buildingRepo = buildingRepo;
        }


        public async Task<IEnumerable<Building>> getBuildingsAsync(IEnumerable<Coordinate> area)
        {
            //Проверка размера области 
            var list = area.ToList();
            if (CalcSquare(list) > 0.002d)
            {
                throw new ArgumentException("Square too big");
            }

            var buildingsList = _buildingRepo.Buildings; //Получение зданий и участков

            return buildingsList.Where(x => isInArea(list, x.Center));
        }

        private bool isInArea(List<Coordinate> list, Coordinate dot)
        {
            var result = false;
            int j = list.Count() - 1;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].Lat < dot.Lat && list[j].Lat >= dot.Lat || list[j].Lat < dot.Lat && list[i].Lat >= dot.Lat)
                {
                    if (list[i].Lng + (dot.Lat - list[i].Lat) / (list[j].Lat - list[i].Lat) *
                        (list[j].Lng - list[i].Lng) < dot.Lng)
                    {
                        result = !result;
                    }
                }

                j = i;
            }

            return result;
        }

        private double CalcSquare(List<Coordinate> area)
        {
            var sum1 = 0d;
            var sum2 = 0d;
            for (int i = 0; i < area.Count - 1; i++)
            {
                sum1 += area[i].Lat * area[i + 1].Lng;
                sum2 += area[i + 1].Lat * area[i].Lng;
            }

            var square = sum1 + area[area.Count - 1].Lat * area[0].Lng - sum2 -
                         area[0].Lat * area[area.Count - 1].Lng;
            return Math.Abs(square) / 2;
        }
    }
}
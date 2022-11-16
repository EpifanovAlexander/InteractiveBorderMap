using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotSpatial.Projections;
using InteractiveBorderMapApp.Entities;
using NetTopologySuite.IO.Esri;
using NetTopologySuite.IO.Esri.Dbf;
using OfficeOpenXml;
using Coordinate = NetTopologySuite.Geometries.Coordinate;

namespace InteractiveBorderMapApp
{
    public class Parser
    {
        public static List<Building> ParseShp(string shpPath, Dictionary<string, Building> buildings)
        {
            foreach (var feature in Shapefile.ReadAllFeatures(shpPath))
            {
                foreach (var geometry in feature.Geometry.Coordinates)
                {
                    buildings[feature.Attributes["cadnum"].ToString()].Coordinates
                        .Add(new Entities.Coordinate(ReprojectFromMsk77(geometry)[1], ReprojectFromMsk77(geometry)[0]));
                }
                buildings[feature.Attributes["cadnum"].ToString()].CalcCenter();
            }
            return buildings.Values.ToList();
        }


        public static void ParseBuildingFromDbfAndShp(string dbfPath)
        {
            using (var dbf = new DbfReader(dbfPath))
            {
                foreach (var fields in dbf)
                {
                    var fieldNames = fields.Keys;
                    foreach (var fieldName in fieldNames)
                    {
                        Console.WriteLine($"{fieldName,10} {fields[fieldName]}");
                    }

                    Console.WriteLine();
                }
            }
        }

        public static Dictionary<string, Building> ParseBuildingsExcel(string excelPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Dictionary<string, Building> result = new Dictionary<string, Building>();
            using (var package = new ExcelPackage(new FileInfo(excelPath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row; //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    var num = worksheet.Cells[row, 1].Value?.ToString().Trim();
                    var address = worksheet.Cells[row, 2].Value?.ToString().Trim();
                    var area = Double.Parse(worksheet.Cells[row, 3].Value?.ToString().Trim());
                    var living = worksheet.Cells[row, 4].Value?.ToString().Trim() == "жилое" ? true : false;
                    var year = worksheet.Cells[row, 5].Value?.ToString().Trim();
                    var material = worksheet.Cells[row, 6].Value?.ToString().Trim();
                    var emerg = worksheet.Cells[row, 7].Value?.ToString().Trim() == "Да" ? true : false;
                    var type = worksheet.Cells[row, 7].Value?.ToString().Trim() == "Да" ? true : false;
                    try
                    {
                        result.Add(num, new Building(num, address, area, living, year, emerg, type, material));
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }

            return result;
        }

        public static List<Area> ParseAreasExcel(string excelPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Area> result = new List<Area>();
            using (var package = new ExcelPackage(new FileInfo(excelPath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row; //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    var dist = worksheet.Cells[row, 1].Value?.ToString().Trim();
                    var num = worksheet.Cells[row, 2].Value?.ToString().Trim();
                    var custom = worksheet.Cells[row, 3].Value?.ToString().Trim() == "Да" ? true : false;
                    var VRI = worksheet.Cells[row, 4].Value?.ToString().Trim() == "Да" ? true : false;
                    var emerg = worksheet.Cells[row, 5].Value?.ToString().Trim() == "Да" ? true : false;

                    result.Add(new Area(dist, num, custom, VRI, emerg));
                }
            }

            return result;
        }

        private static double[] ReprojectFromMsk77(Coordinate coordinate)
        {
            var proj4_msk77 =
                "+proj=tmerc +lat_0=55.66666666667 +lon_0=37.5 +k=1 +x_0=11 +y_0=10 +ellps=bessel +towgs84=316.151,78.924,589.65,-1.57273,2.69209,2.34693,8.4507 +units=m +no_defs";
            var proj4_new =
                "+proj=longlat +ellps=WGS84 +datum=WGS84 +no_defs ";

            var src =
                ProjectionInfo.FromProj4String(proj4_msk77);
            var dest =
                ProjectionInfo.FromProj4String(proj4_new);

            double[] xy = new[] { coordinate.X, coordinate.Y };

            Reproject.ReprojectPoints(xy, new[] { 0.0d }, src, dest, 0, 1);
            return xy;
        }
    }
}
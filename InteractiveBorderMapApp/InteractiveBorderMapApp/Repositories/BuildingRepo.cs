using System.Collections.Generic;
using System.IO;

namespace InteractiveBorderMapApp.Repositories
{
    public class BuildingRepo
    {
        private string _shpPath = @"Dataset\ОКС\ОКС.shp";
        private string _excelPath = @"Dataset\Здания СВАО_САО жилое_нежилое.xlsx";
        public readonly List<Building> Buildings;

        public BuildingRepo()
        {
            Buildings = Parser.ParseShp(Path.GetFullPath(_shpPath), Parser.ParseBuildingsExcel(Path.GetFullPath(_excelPath)));
        }

    }
}
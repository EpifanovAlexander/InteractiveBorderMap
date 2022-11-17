using System.Collections.Generic;
using System.IO;

namespace InteractiveBorderMapApp.Repositories
{
    public class BuildingRepo
    {
        private static char SEP = Path.DirectorySeparatorChar;
        private string _shpPath = "Dataset" + SEP + "ОКС" + SEP + "ОКС.shp";
        private string _excelPath = "Dataset" + SEP + "Здания СВАО_САО жилое_нежилое.xlsx";
        public readonly List<Building> Buildings;

        public BuildingRepo()
        {
            Buildings = Parser.ParseShp(Path.GetFullPath(_shpPath),
                Parser.ParseBuildingsExcel(Path.GetFullPath(_excelPath)));
        }
    }
}
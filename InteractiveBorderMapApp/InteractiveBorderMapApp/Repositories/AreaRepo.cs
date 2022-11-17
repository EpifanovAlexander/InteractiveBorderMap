using System.Collections.Generic;
using System.IO;

namespace InteractiveBorderMapApp.Repositories
{
    public class AreaRepo
    {
        private string _shpPath = @"Dataset\ЗУ\Земельные_участки.shp";
        private string _excelPath = @"Dataset\Аварийные_Самовольные_Несоответствие_ВРИ_СВАО_САО.XLSX";
        public readonly List<Area> Areas = new List<Area>();

        public AreaRepo()
        {
            Areas = Parser.ParseAreasShp(Path.GetFullPath(_shpPath));
        }
    }
}

using GISDataParser;


var dbfPath = @"C:\Users\npcdo\Downloads\Датасет №5\Датасет\Организации СВАО_САО\Организации_СВАО_САО.dbf";
Parser.ParseDbf(dbfPath);

var shpPath =
    @"C:\Users\npcdo\Downloads\Датасет №5\Датасет\Организации СВАО_САО\Организации_СВАО_САО.shp";
Parser.ParseShp(shpPath);    

var excelPath = @"C:\Users\npcdo\Downloads\Датасет №5\Датасет\Здания СВАО_САО жилое_нежилое.xlsx";
foreach (var vBuilding in Parser.ParseBuildingsExcel(excelPath))
{
    Console.WriteLine(vBuilding);
}

var excelAreaPath = @"C:\Users\npcdo\Downloads\Датасет №5\Датасет\Аварийные_Самовольные_Несоответствие_ВРИ_СВАО_САО.XLSX";
foreach (var vArea in Parser.ParseAreasExcel(excelAreaPath))
{
    Console.WriteLine(vArea);
}
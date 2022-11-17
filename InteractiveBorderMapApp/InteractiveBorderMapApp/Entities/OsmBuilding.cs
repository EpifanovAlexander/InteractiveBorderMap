using System.Collections.Generic;
using InteractiveBorderMapApp.Services;

namespace InteractiveBorderMapApp.Entities
{
    public class OsmBuilding
    {
        public Coordinate Center { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public string Solution { get; set; }
        public string Content { get; set; }
        public string Marker { get; set; }
        public Building Building { get; set; }

        public OsmBuilding(CriteriaService criteriaService, Building building)
        {
            Center = building.Center;
            Coordinates = building.Coordinates;
            Building = building;
            var criteria = criteriaService.DoCheck(building);
            if (criteria >= 50d)
            {
                Solution = "Включение объекта";
                Content = $"<br>Соответствие критериям: {criteria}<br>";
                Marker = MarkerType.INCLUDE;
            }
            if (criteria >= 19d && criteria < 50d)
            {
                Solution = "На обсуждение";
                Content = $"<br>Соответствие критериям: {criteria}<br>";
                Marker = MarkerType.DISCUSS;
            }
            if (criteria < 19d)
            {
                Solution = "Объект не вкючен";
                Content = $"<br>Соответствие критериям: {criteria}<br>";
                Marker = MarkerType.EXCLUDE;
            }
        }
    }
}
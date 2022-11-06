namespace InteractiveBorderMapApp.Entities
{
    public class OsmBuilding
    {
        public Coordinate Coordinate { get; set; }
        public string Content { get; set; }

        public OsmBuilding()
        {
            Coordinate = new Coordinate();
        }
    }
}
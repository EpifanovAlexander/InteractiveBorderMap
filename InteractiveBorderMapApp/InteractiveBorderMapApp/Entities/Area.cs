using System.Collections.Generic;
using InteractiveBorderMapApp.Entities;

public class Area
{
    public string District;
    public string Number;
    public bool IsCustom;
    public bool IsVRI;
    public bool IsEmergency;
    public int Property;
    public string Address;

    public List<Coordinate> Coordinates { get; set; }
    public Coordinate Center { get; set; }

   
    

    public Area(string district, string number, bool isCustom, bool isVri, bool isEmergency)
    {
        District = district;
        Number = number;
        IsCustom = isCustom;
        IsVRI = isVri;
        IsEmergency = isEmergency;
       
    }

    public Area()
    {
        Center = new Coordinate();
        Property = 0;
        Coordinates = new List<Coordinate>();
    }

    public void CalcCenter()
    {
        var sumX = 0d;
        var sumY = 0d;
        foreach (var coordinate in Coordinates)
        {
            sumX += coordinate.Lat;
            sumY += coordinate.Lng;
        }
        Center = new Coordinate(sumX / Coordinates.Count, sumY / Coordinates.Count);
    }

    public override string ToString()
    {
        return $"{nameof(Number)}: {Number}, {nameof(Property)}: {Property}";
    }
}
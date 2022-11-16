using System.Collections.Generic;
using InteractiveBorderMapApp.Entities;

public class Building
{
    public string Number;
    public string Address;
    public double Area;
    public bool IsLiving;
    public string Year;
    public bool IsEmergency;
    public bool IsType;
    public string Material;

    public List<Coordinate> Coordinates;
    public Coordinate Center;

    public Building(string number, string address, double area, bool isLiving, string year, bool isEmergency,
        bool isType, string material)
    {
        Number = number;
        Address = address;
        Area = area;
        IsLiving = isLiving;
        Year = year;
        IsEmergency = isEmergency;
        IsType = isType;
        Material = material;
        Coordinates = new List<Coordinate>();
        Center = new Coordinate();
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
        return $"{nameof(Address)}: {Address}";
    }
}
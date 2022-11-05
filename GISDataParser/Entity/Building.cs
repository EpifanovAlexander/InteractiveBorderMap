namespace GISDataParser.Entity;

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
    }

    public override string ToString()
    {
        return
            $"{nameof(Number)}: {Number}, {nameof(Address)}: {Address}, {nameof(Area)}: {Area}, {nameof(IsLiving)}: {IsLiving}, {nameof(Year)}: {Year}, {nameof(IsEmergency)}: {IsEmergency}, {nameof(IsType)}: {IsType}, {nameof(Material)}: {Material}";
    }
}
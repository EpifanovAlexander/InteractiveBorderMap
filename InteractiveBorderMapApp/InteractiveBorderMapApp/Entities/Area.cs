public class Area
{
    public string District;
    public string Number;
    public bool IsCustom;
    public bool IsVRI;
    public bool IsEmergency;

    

    public Area(string district, string number, bool isCustom, bool isVri, bool isEmergency)
    {
        District = district;
        Number = number;
        IsCustom = isCustom;
        IsVRI = isVri;
        IsEmergency = isEmergency;
    }

    public override string ToString()
    {
        return $"{nameof(District)}: {District}, {nameof(Number)}: {Number}, {nameof(IsCustom)}: {IsCustom}, {nameof(IsVRI)}: {IsVRI}, {nameof(IsEmergency)}: {IsEmergency}";
    }
}
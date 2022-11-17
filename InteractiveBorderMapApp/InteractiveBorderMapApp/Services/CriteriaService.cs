namespace InteractiveBorderMapApp.Services
{
    public class CriteriaService
    {
        private const double CRITERIA_WEIGHT_LIVING = 35d;
        private const double LIVING_EMERGENCY = 50d;
        private const double LIVING_RENOVATION = 25d;
        private const double LIVING_TYPICAL_POJ = 25d;
        private const double CRITERIA_WEIGHT_WORKING = 10d;
        private const double BF100 = 50d;
        private const double BTWN100N1000 = 30d;
        private const double MORE1000 = 20d;
        private const double CRITERIA_WEIGHT_RIGHTS = 17.5d;
        private const double OKN = 20d;
        private const double SDZ = 20d;
        private const double RENT = 60d;
        
        private const double NL_CRITERIA_WEIGHT = 35d;
        private const double NL_EMERGENCY = 20d;
        private const double NL_NOT_MATCH_VRI = 40d;
        private const double NL_SELFBUILD = 40d;
        private const double NL_CRITERIA_WEIGHT_WORKING = 35d;
        private const double NL_BF100 = 50d;
        private const double NL_BTWN100N1000 = 30d;
        private const double NL_1000MORE = 20d;
        private const double NL_CRITERIA_WEIGHT_RIGHTS = 30d;
        private const double NL_OKN = 20d;
        private const double NL_SDZ = 20d;
        private const double NL_RENT = 60d;
        
        
        public CriteriaService()
        {
        }

        public double DoCheck(Building building)
        {
            double weight = 0d;
            if (building.IsLiving)
            {
                if (building.IsEmergency) weight += LIVING_EMERGENCY;
                if (building.IsType) weight += LIVING_TYPICAL_POJ;
            }
            else
            {
                weight += 25;
                if (building.IsEmergency) weight += LIVING_EMERGENCY;
            }

            return weight;
        }
        
    }
}
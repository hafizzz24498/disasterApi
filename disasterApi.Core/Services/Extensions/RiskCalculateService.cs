using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Services.Extensions
{
    public class RiskCalculateService
    {
        public static int CalculateFloodRisk(double rainfall)
        {
            if (rainfall > 50) return 100; 
            else if (rainfall > 20) return 70;
            return 30;
        }

        public static int CalculateEarthquakeRisk(double magnitude)
        {
            if (magnitude >= 5.0) return 100;
            else if (magnitude >= 3.0) return 70;
            else return 30;
        }

        public static int CalculateWildfireRisk(double temperature, double humidity)
        {
            if (temperature > 30 && humidity < 40) return 100;
            else if (temperature > 25 && humidity < 50) return 70;
            else return 30;
        }

        public static string GetRiskLevel(int riskScore)
        {
            if (riskScore >= 75) return "High";
            else if (riskScore >= 50) return "Medium";
            else return "No Risk";
        }
    }
}

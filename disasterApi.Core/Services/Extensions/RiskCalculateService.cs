using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Services.Extensions
{
    public class RiskCalculateService
    {
        public int CalculateFloodRisk(double rainfall)
        {
            if (rainfall > 50) return 100;
            else if (rainfall > 20) return 75;
            else if (rainfall > 10) return 50;
            else if (rainfall > 5) return 25;
            else return 0;
        }

        public int CalculateEarthquakeRisk(double magnitude)
        {
            if (magnitude >= 7.0) return 100;
            else if (magnitude >= 5.0) return 75;
            else if (magnitude >= 4.0) return 50;
            else if (magnitude >= 3.0) return 25;
            else return 0;
        }

        public int CalculateWildfireRisk(double temperature, double humidity)
        {
            if (temperature > 35 && humidity < 30) return 100;
            else if (temperature > 30 && humidity < 40) return 75;
            else if (temperature > 25 && humidity < 50) return 50;
            else if (temperature > 20 && humidity < 60) return 25;
            else return 0;
        }

        public string GetRiskLevel(int riskScore)
        {
            if (riskScore >= 75) return "High Risk";
            else if (riskScore >= 50) return "Moderate Risk";
            else if (riskScore >= 25) return "Low Risk";
            else return "No Risk";
        }
    }
}

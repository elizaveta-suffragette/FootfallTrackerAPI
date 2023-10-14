using FootfallTracker.Models;

namespace FootfallTracker.Logic
{
    public class AggregationService : IAggregationService
    {
        public IEnumerable<HourlyAggregation> GetHourlyData(string filePath, DateTime startDate, DateTime endDate)
        {
            var footfallData = ReadCsvData(filePath);

            var filteredData = footfallData
                .Where(entry => entry.Timestamp >= startDate && entry.Timestamp <= endDate)
                .ToList();

            if (filteredData.Count == 0)
            {
                return Enumerable.Empty<HourlyAggregation>();
            }

            var hourlyAggregations = new List<HourlyAggregation>(); //TODO map

            return hourlyAggregations;
        }

        private IEnumerable<FootfallRecord> ReadCsvData(string filePath)
        {
            var csvData = new List<FootfallRecord>();
            return csvData;
        }
    }
}

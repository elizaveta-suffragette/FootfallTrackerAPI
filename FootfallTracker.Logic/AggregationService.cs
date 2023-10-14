using CsvHelper;
using CsvHelper.Configuration;
using FootfallTracker.Models;
using System.Globalization;

namespace FootfallTracker.Logic
{
    public class AggregationService : IAggregationService
    {
        public async Task<IEnumerable<AggregatedData>> GetAggregatedData(string timeframe, string filePath, DateTime startDate, DateTime endDate)
        {
            var footfallData = await AggregationServiceHelper.ReadCsvData(filePath);

            var filteredData = footfallData
                .Where(entry => entry.TimeStamp >= startDate && entry.TimeStamp <= endDate)
                .ToList();

            IEnumerable<AggregatedData> aggregatedData = new List<AggregatedData>();

            if (timeframe == "hourly")
            {
                aggregatedData = AggregationServiceHelper.AggregateHourly(filteredData);
            }
            else if (timeframe == "daily")
            {
                aggregatedData = AggregationServiceHelper.AggregateDaily(filteredData);
            }
            else if (timeframe == "weekly")
            {
                aggregatedData = AggregationServiceHelper.AggregateWeekly(filteredData);
            }

            return aggregatedData;
        }
    }
}

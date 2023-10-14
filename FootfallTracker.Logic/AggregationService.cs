using FootfallTracker.Models;

namespace FootfallTracker.Logic
{
    public class AggregationService : IAggregationService
    {
        public async Task<IEnumerable<FootfallRecord>> GetAggregatedData(string timeframe, string filePath, DateTime startDate, DateTime endDate)
        {
            var footfallData = await ReadCsvData(filePath);

            var filteredData = footfallData
                .Where(entry => entry.Timestamp >= startDate && entry.Timestamp <= endDate)
                .ToList();

            var aggregatedData = new List<FootfallRecord>();

            if (timeframe == "hourly")
            {
                aggregatedData = AggregateHourly(filteredData);
            }
            else if (timeframe == "daily")
            {
                aggregatedData = AggregateDaily(filteredData);
            }
            else if (timeframe == "weekly")
            {
                aggregatedData = AggregateWeekly(filteredData);
            }

            return aggregatedData;
        }

        private List<FootfallRecord> AggregateWeekly(List<FootfallRecord> filteredData)
        {
            throw new NotImplementedException();
        }

        private List<FootfallRecord> AggregateDaily(List<FootfallRecord> filteredData)
        {
            throw new NotImplementedException();
        }

        private List<FootfallRecord> AggregateHourly(List<FootfallRecord> filteredData)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<FootfallRecord>> ReadCsvData(string filePath)

        {
            var csvData = new List<FootfallRecord>();
            return csvData;
        }
    }
}

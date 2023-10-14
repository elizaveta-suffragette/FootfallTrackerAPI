using FootfallTracker.Models;

namespace FootfallTracker.Logic
{
    public class AggregationService : IAggregationService
    {
        public async Task<IEnumerable<AggregatedData>> GetAggregatedData(string timeframe, string filePath, DateTime startDate, DateTime endDate)
        {
            var footfallData = await ReadCsvData(filePath);

            var filteredData = footfallData
                .Where(entry => entry.Timestamp >= startDate && entry.Timestamp <= endDate)
                .ToList();

            IEnumerable<AggregatedData> aggregatedData = new List<AggregatedData>();

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

        private IEnumerable<AggregatedData> AggregateHourly(List<FootfallRecord> data)
        {
            var hourlyAggregations = data
                .GroupBy(entry => new
                {
                    entry.Timestamp.Date,
                    entry.Timestamp.Hour
                })
                .Select(group => new AggregatedData
                {
                    Date = group.Key.Date,
                    Hour = group.Key.Hour,
                    TotalCount = group.Sum(entry => entry.Count)
                })
                .ToList();

            return hourlyAggregations;
        }

        private IEnumerable<AggregatedData> AggregateDaily(List<FootfallRecord> data)
        {
            var dailyAggregations = data
                .GroupBy(entry => entry.Timestamp.Date)
                .Select(group => new AggregatedData
                {
                    Date = group.Key,
                    TotalCount = group.Sum(entry => entry.Count)
                })
                .ToList();

            return dailyAggregations;
        }

        private IEnumerable<AggregatedData> AggregateWeekly(List<FootfallRecord> data)
        {
            var weeklyAggregations = data
                .GroupBy(entry => GetStartOfWeek(entry.Timestamp))
                .Select(group => new AggregatedData
                {
                    Date = group.Key,
                    TotalCount = group.Sum(entry => entry.Count)
                })
                .ToList();

            return weeklyAggregations;
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            var daysToSubtract = (int)dayOfWeek;
            return date.Date.AddDays(-daysToSubtract);
        }

        private async Task<IEnumerable<FootfallRecord>> ReadCsvData(string filePath)
        {
            var csvData = new List<FootfallRecord>();
            return csvData;
        }
    }
}

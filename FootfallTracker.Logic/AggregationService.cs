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
            var footfallData = await ReadCsvData(filePath);

            var filteredData = footfallData
                .Where(entry => entry.TimeStamp >= startDate && entry.TimeStamp <= endDate)
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
                    entry.TimeStamp.Date,
                    entry.TimeStamp.Hour
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
                .GroupBy(entry => entry.TimeStamp.Date)
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
                .GroupBy(entry => GetStartOfWeek(entry.TimeStamp))
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

        public async Task<IEnumerable<FootfallRecord>> ReadCsvData(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            csv.Context.RegisterClassMap<FootfallRecordMap>();

            var records = new List<FootfallRecord>();
            await foreach (var record in csv.GetRecordsAsync<FootfallRecord>())
            {
                records.Add(record);
            }

            return records;
        }
    }
}

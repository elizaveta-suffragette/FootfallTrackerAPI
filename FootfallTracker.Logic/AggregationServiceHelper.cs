﻿using CsvHelper;
using CsvHelper.Configuration;
using FootfallTracker.Models;
using System.Globalization;

namespace FootfallTracker.Logic
{
    public class AggregationServiceHelper
    {
        public static IEnumerable<AggregatedData> AggregateHourly(List<FootfallRecord> data)
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

        public static IEnumerable<AggregatedData> AggregateDaily(List<FootfallRecord> data)
        {
            var dailyAggregations = data
                .GroupBy(entry => new
                {
                    Date = entry.TimeStamp.Date,
                })
                .Select(group => new AggregatedData
                {
                    Date = group.Key.Date,
                    TotalCount = group.Sum(entry => entry.Count),
                    Hour = 0,
                    Day = group.Key.Date.Day,
                    Week = CalculateWeekNumber(group.Key.Date),
                })
                .ToList();

            return dailyAggregations;
        }

        public static int CalculateWeekNumber(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Sunday)
            {
                return 1;
            }
            else
            {
                int dayOffset = (int)dayOfWeek;
                return (date.Day + 6 - dayOffset) / 7;
            }
        }

        public static IEnumerable<AggregatedData> AggregateWeekly(List<FootfallRecord> data)
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

        public static DateTime GetStartOfWeek(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            var daysToSubtract = (int)dayOfWeek;
            return date.Date.AddDays(-daysToSubtract);
        }

        public static async Task<IEnumerable<FootfallRecord>> ReadCsvData(string filePath)
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

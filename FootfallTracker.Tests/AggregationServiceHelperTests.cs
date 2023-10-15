using FootfallTracker.Logic;
using FootfallTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FootfallTracker.Tests
{
    public class AggregationServiceHelperTests
    {
        [Fact]
        public void AggregateHourly_CorrectData_ReturnsHourlyAggregations()
        {
            // Arrange
            var data = new List<FootfallRecord>
            {
                new FootfallRecord { TimeStamp = DateTime.Parse("2023-01-01T00:00:00"), Count = 10 },
                new FootfallRecord { TimeStamp = DateTime.Parse("2023-01-01T01:00:00"), Count = 20 },
                new FootfallRecord { TimeStamp = DateTime.Parse("2023-01-01T02:00:00"), Count = 15 },
                new FootfallRecord { TimeStamp = DateTime.Parse("2023-01-01T03:00:00"), Count = 25 }
            };

            // Act
            var result = AggregationServiceHelper.AggregateHourly(data);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count());

            var expectedResults = new List<(DateTime Date, int Hour, int TotalCount, int Day, int Week)>
            {
                (DateTime.Parse("2023-01-01T00:00:00"), 0, 10, 1, 1),
                (DateTime.Parse("2023-01-01T01:00:00"), 1, 20, 1, 1),
                (DateTime.Parse("2023-01-01T02:00:00"), 2, 15, 1, 1),
                (DateTime.Parse("2023-01-01T03:00:00"), 3, 25, 1, 1),
            };

            Assert.True(expectedResults.SequenceEqual(result.Select(r =>
                (r.Date, r.Hour, r.TotalCount, r.Day, r.Week))));
        }
    }
}


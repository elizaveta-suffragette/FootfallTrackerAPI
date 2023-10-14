using FootfallTracker.Logic;
using FootfallTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FootfallTracker.Tests
{
    public class AggregationServiceTests
    {
        [Fact]
        public async Task GetAggregatedData_CorrectData_ReturnsHourlyAggregations()
        {
            // Arrange
            var service = new AggregationService();
            var timeframe = "hourly";
            var filePath = Path.Combine("FootfallTracker.Data", "footfall_data.csv");
            var startDate = DateTime.Parse("2023-01-01");
            var endDate = DateTime.Parse("2023-01-02");

            // Act
            var result = await service.GetAggregatedData(timeframe, filePath, startDate, endDate);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void AggregateHourly_CorrectData_ReturnsHourlyAggregations()
        {
            // Arrange
            var service = new AggregationService();
            var data = new List<FootfallRecord>
        {
            new FootfallRecord { TimeStamp = DateTime.Parse("2023-01-01 10:00:00"), Count = 5 },
            new FootfallRecord { TimeStamp = DateTime.Parse("2023-01-01 11:00:00"), Count = 10 }
        };

            // Act
            var result = AggregationServiceHelper.AggregateHourly(data);

            // Assert
            Assert.NotNull(result);
        }
    }
}

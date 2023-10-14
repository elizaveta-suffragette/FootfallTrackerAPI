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
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataFilePath = Path.Combine(baseDirectory, "footfall_data.csv");
            var startDate = DateTime.Parse("2023-01-01");
            var endDate = DateTime.Parse("2023-01-02");

            // Act
            var result = await service.GetAggregatedData(timeframe, dataFilePath, startDate, endDate);

            // Assert
            Assert.NotNull(result);
        }
    }
}

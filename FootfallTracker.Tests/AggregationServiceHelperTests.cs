using FootfallTracker.Logic;
using FootfallTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FootfallTracker.Tests
{
    public class AggregationServiceHelperTests
    {

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

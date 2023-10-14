using FootfallTracker.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FootfallTrackerAPI.Controllers
{
    [Route("api/aggregation")]
    public class AggregationController : ControllerBase
    {
        private readonly IAggregationService _aggregationService;

        public AggregationController(IAggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        [HttpGet]
        /// <summary>
        /// Get hourly aggregated data
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Hourly aggregated data</returns>
        public async Task<IActionResult> GetAggregatedData(string timeframe, string filePath, DateTime startDate, DateTime endDate)
            {
            var aggregatedData = await _aggregationService.GetAggregatedData(timeframe, filePath, startDate, endDate);

            if (aggregatedData == null)
            {
                return NotFound("No data available for the specified date range.");
            }

            return Ok(aggregatedData);
        }
    }
}

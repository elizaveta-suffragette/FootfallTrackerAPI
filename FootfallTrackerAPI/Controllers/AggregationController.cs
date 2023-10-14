using FootfallTracker.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FootfallTrackerAPI.Controllers
{
    public class AggregationController : Controller
    {
        private readonly IAggregationService _aggregationService;
        public AggregationController(IAggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        public IActionResult GetHourlyData(string filePath, DateTime startDate, DateTime endDate)
        {
            var hourlyData = _aggregationService.GetHourlyData(filePath, startDate, endDate);

            return Json(hourlyData);
        }
    }
}

using FootfallTracker.Models;

namespace FootfallTracker.Logic
{
    public interface IAggregationService
    {
        Task<IEnumerable<AggregatedData>> GetAggregatedData(string timeframe, string filePath, DateTime startDate, DateTime endDate);
    }
}

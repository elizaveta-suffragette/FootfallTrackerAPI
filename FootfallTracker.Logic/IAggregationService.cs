using FootfallTracker.Models;

namespace FootfallTracker.Logic
{
    public interface IAggregationService
    {
        public Task<IEnumerable<FootfallRecord>> GetAggregatedData(string timeframe, string filePath, DateTime startDate, DateTime endDate);
    }
}

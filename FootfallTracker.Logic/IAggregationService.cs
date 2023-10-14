using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootfallTracker.Logic
{
    public interface IAggregationService
    {
        public IEnumerable<HourlyAggregation> GetHourlyData(string filePath, DateTime startDate, DateTime endDate);
    }
}

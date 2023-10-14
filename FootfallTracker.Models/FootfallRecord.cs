using FootfallTracker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootfallTracker.Models
{
    public class FootfallRecord
    {
        public int SensorId { get; set; }
        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
    }
}

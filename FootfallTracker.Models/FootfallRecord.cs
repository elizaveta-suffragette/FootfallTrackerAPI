namespace FootfallTracker.Models
{
    public class FootfallRecord
    {
        public int SensorId { get; set; }
        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
    }
}

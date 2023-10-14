namespace FootfallTracker.Models
{
    public class AggregatedData
    {
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }
        public int Week { get; set; }
        public int TotalCount { get; set; }
    }
}
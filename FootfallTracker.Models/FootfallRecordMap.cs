using CsvHelper.Configuration;

namespace FootfallTracker.Models
{
    public class FootfallRecordMap : ClassMap<FootfallRecord>
    {
        public FootfallRecordMap()
        {
            Map(m => m.SensorId).Name("SensorId");
            Map(m => m.TimeStamp).Name("TimeStamp");
            Map(m => m.Count).Name("Count");
        }
    }

}

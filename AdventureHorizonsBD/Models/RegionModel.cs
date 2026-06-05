using System;

namespace AdventureHorizonsBD.Models
{
    public class RegionModel
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public string Highlights { get; set; }
        public string Description { get; set; }
        public string PopularTrips { get; set; }
    }
}

using System;

namespace AdventureHorizonsBD.Models
{
    public class EventModel
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string EventDate { get; set; }
        public string EventDuration { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

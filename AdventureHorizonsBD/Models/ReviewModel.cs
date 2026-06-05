using System;

namespace AdventureHorizonsBD.Models
{
    public class ReviewModel
    {
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public string EventName { get; set; }
        public string Title { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ReviewDate { get; set; }

        // Additional fields for displaying UI
        public string UserFullName { get; set; }
    }
}

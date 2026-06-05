using System;

namespace AdventureHorizonsBD.Models
{
    public class EventRegistrationModel
    {
        public int RegistrationID { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }
        public int NumberOfParticipants { get; set; }
        public string SpecialRequests { get; set; }
        public string Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        // Navigation / joined fields
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string EventTitle { get; set; }
        public string EventDate { get; set; }
    }
}

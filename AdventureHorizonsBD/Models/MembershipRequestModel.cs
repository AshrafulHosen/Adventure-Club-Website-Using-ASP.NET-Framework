using System;

namespace AdventureHorizonsBD.Models
{
    public class MembershipRequestModel
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public string MembershipType { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        
        // Navigation / joined fields
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}

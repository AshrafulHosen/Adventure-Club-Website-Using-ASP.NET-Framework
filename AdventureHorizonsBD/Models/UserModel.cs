using System;

namespace AdventureHorizonsBD.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string MembershipPlan { get; set; }
        public string ExperienceLevel { get; set; }
        public string Role { get; set; }
        public bool IsApproved { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

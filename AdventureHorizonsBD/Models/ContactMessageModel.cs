using System;

namespace AdventureHorizonsBD.Models
{
    public class ContactMessageModel
    {
        public int MessageID { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentDate { get; set; }
    }
}

using System;

namespace AdventureHorizonsBD.Models
{
    public class GalleryModel
    {
        public int ImageID { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public int? UploadedByUserID { get; set; }
        public bool IsApproved { get; set; }
        public DateTime UploadDate { get; set; }
        
        // Navigation field
        public string UploadedByUserName { get; set; }
    }
}

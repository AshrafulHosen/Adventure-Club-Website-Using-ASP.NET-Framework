namespace AdventureHorizonsBD {
    public partial class Admin {
        protected global::System.Web.UI.WebControls.Panel pnlAdminLoginSection;
        protected global::System.Web.UI.WebControls.Panel pnlAdminLoginForm;
        protected global::System.Web.UI.WebControls.TextBox txtAdminEmail;
        protected global::System.Web.UI.WebControls.TextBox txtAdminPassword;
        protected global::System.Web.UI.WebControls.Label lblAdminLoginMsg;
        protected global::System.Web.UI.WebControls.Button btnAdminLogin;
        
        protected global::System.Web.UI.WebControls.Panel pnlAdminDashboard;
        protected global::System.Web.UI.WebControls.LinkButton btnAdminLogout;
        
        protected global::System.Web.UI.WebControls.Literal litStatMembers;
        protected global::System.Web.UI.WebControls.Literal litStatPending;
        protected global::System.Web.UI.WebControls.Literal litStatEvents;
        protected global::System.Web.UI.WebControls.Literal litStatMessages;
        
        protected global::System.Web.UI.WebControls.Repeater rptMembershipRequests;
        protected global::System.Web.UI.HtmlControls.HtmlTableRow rowNoRequests;
        
        protected global::System.Web.UI.WebControls.Repeater rptApprovedMembers;
        protected global::System.Web.UI.WebControls.Label lblNoApprovedMembers;
        
        protected global::System.Web.UI.WebControls.TextBox txtAdminEventTitle;
        protected global::System.Web.UI.WebControls.TextBox txtAdminEventDate;
        protected global::System.Web.UI.WebControls.TextBox txtAdminEventDuration;
        protected global::System.Web.UI.WebControls.TextBox txtAdminEventRegion;
        protected global::System.Web.UI.WebControls.TextBox txtAdminEventDescription;
        protected global::System.Web.UI.WebControls.Label lblAdminEventMsg;
        protected global::System.Web.UI.WebControls.Button btnAdminAddEvent;
        
        protected global::System.Web.UI.WebControls.Repeater rptAdminEvents;
        protected global::System.Web.UI.WebControls.Label lblNoEvents;
        
        protected global::System.Web.UI.WebControls.TextBox txtAdminPhotoTitle;
        protected global::System.Web.UI.WebControls.FileUpload fileAdminPhoto;
        protected global::System.Web.UI.WebControls.TextBox txtAdminPhotoUrl;
        protected global::System.Web.UI.WebControls.TextBox txtAdminPhotoDescription;
        protected global::System.Web.UI.WebControls.Label lblAdminGalleryMsg;
        protected global::System.Web.UI.WebControls.Button btnAdminAddPhoto;
        
        protected global::System.Web.UI.WebControls.Repeater rptAdminGallery;
        protected global::System.Web.UI.WebControls.Label lblNoGallery;
        
        protected global::System.Web.UI.WebControls.Repeater rptContactMessages;
        
        protected global::System.Web.UI.WebControls.Repeater rptAdminBookings;
        protected global::System.Web.UI.WebControls.Label lblNoBookings2;
        protected global::System.Web.UI.WebControls.Repeater rptAdminReviews;
        protected global::System.Web.UI.WebControls.Label lblNoReviews;
    }
}

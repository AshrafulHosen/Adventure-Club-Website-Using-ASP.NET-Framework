namespace AdventureHorizonsBD
{
    public partial class Admin
    {
        // Login
        protected global::System.Web.UI.WebControls.Panel    pnlAdminLoginSection;
        protected global::System.Web.UI.WebControls.Panel    pnlAdminLoginForm;
        protected global::System.Web.UI.WebControls.TextBox  txtAdminEmail;
        protected global::System.Web.UI.WebControls.TextBox  txtAdminPassword;
        protected global::System.Web.UI.WebControls.Label    lblAdminLoginMsg;
        protected global::System.Web.UI.WebControls.Button   btnAdminLogin;

        // Dashboard
        protected global::System.Web.UI.WebControls.Panel       pnlAdminDashboard;
        protected global::System.Web.UI.WebControls.LinkButton   btnAdminLogout;
        protected global::System.Web.UI.WebControls.Literal      litStatMembers;
        protected global::System.Web.UI.WebControls.Literal      litStatPending;
        protected global::System.Web.UI.WebControls.Literal      litStatEvents;
        protected global::System.Web.UI.WebControls.Literal      litStatMessages;

        // Members
        protected global::System.Web.UI.WebControls.Repeater         rptMembershipRequests;
        protected global::System.Web.UI.HtmlControls.HtmlTableRow    rowNoRequests;
        protected global::System.Web.UI.WebControls.Repeater         rptApprovedMembers;
        protected global::System.Web.UI.WebControls.Label            lblNoApprovedMembers;

        // Events – Add form
        protected global::System.Web.UI.WebControls.TextBox  txtAdminEventTitle;
        protected global::System.Web.UI.WebControls.TextBox  txtAdminEventDate;
        protected global::System.Web.UI.WebControls.TextBox  txtAdminEventDuration;
        protected global::System.Web.UI.WebControls.TextBox  txtAdminEventRegion;
        protected global::System.Web.UI.WebControls.TextBox  txtAdminEventDescription;
        protected global::System.Web.UI.WebControls.Label    lblAdminEventMsg;
        protected global::System.Web.UI.WebControls.Button   btnAdminAddEvent;
        protected global::System.Web.UI.WebControls.Repeater rptAdminEvents;
        protected global::System.Web.UI.WebControls.Label    lblNoEvents;

        // Events – Edit panel
        protected global::System.Web.UI.WebControls.Panel       pnlEditEvent;
        protected global::System.Web.UI.WebControls.HiddenField hdnEditEventID;
        protected global::System.Web.UI.WebControls.TextBox     txtEditEventTitle;
        protected global::System.Web.UI.WebControls.TextBox     txtEditEventDate;
        protected global::System.Web.UI.WebControls.TextBox     txtEditEventDuration;
        protected global::System.Web.UI.WebControls.TextBox     txtEditEventRegion;
        protected global::System.Web.UI.WebControls.TextBox     txtEditEventDescription;
        protected global::System.Web.UI.WebControls.Label       lblEditEventMsg;
        protected global::System.Web.UI.WebControls.Button      btnSaveEvent;
        protected global::System.Web.UI.WebControls.Button      btnCancelEditEvent;

        // Gallery – Add form
        protected global::System.Web.UI.WebControls.TextBox      txtAdminPhotoTitle;
        protected global::System.Web.UI.WebControls.FileUpload   fileAdminPhoto;
        protected global::System.Web.UI.WebControls.TextBox      txtAdminPhotoUrl;
        protected global::System.Web.UI.WebControls.TextBox      txtAdminPhotoDescription;
        protected global::System.Web.UI.WebControls.Label        lblAdminGalleryMsg;
        protected global::System.Web.UI.WebControls.Button       btnAdminAddPhoto;
        protected global::System.Web.UI.WebControls.Repeater     rptAdminGallery;
        protected global::System.Web.UI.WebControls.Label        lblNoGallery;

        // Gallery – Edit panel
        protected global::System.Web.UI.WebControls.Panel       pnlEditImage;
        protected global::System.Web.UI.WebControls.HiddenField hdnEditImageID;
        protected global::System.Web.UI.WebControls.HiddenField hdnEditImageURL;
        protected global::System.Web.UI.WebControls.TextBox     txtEditImageTitle;
        protected global::System.Web.UI.WebControls.TextBox     txtEditImageDescription;
        protected global::System.Web.UI.WebControls.FileUpload  fileEditPhoto;
        protected global::System.Web.UI.WebControls.Image       imgEditPreview;
        protected global::System.Web.UI.WebControls.Label       lblEditImageMsg;
        protected global::System.Web.UI.WebControls.Button      btnSaveImage;
        protected global::System.Web.UI.WebControls.Button      btnCancelEditImage;

        // Messages
        protected global::System.Web.UI.WebControls.Repeater rptContactMessages;

        // Bookings
        protected global::System.Web.UI.WebControls.Repeater rptAdminBookings;
        protected global::System.Web.UI.WebControls.Label    lblNoBookings2;

        // Reviews
        protected global::System.Web.UI.WebControls.Repeater rptAdminReviews;
        protected global::System.Web.UI.WebControls.Label    lblNoAdminReviews;

        // Tab state
        protected global::System.Web.UI.WebControls.HiddenField hdnActiveAdminTab;
    }
}

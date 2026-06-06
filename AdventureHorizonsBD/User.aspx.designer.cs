namespace AdventureHorizonsBD {
    public partial class User {
        protected global::System.Web.UI.WebControls.Panel pnlUserLoginSection;
        protected global::System.Web.UI.WebControls.Panel pnlUserLoginForm;
        protected global::System.Web.UI.WebControls.TextBox txtUserEmail;
        protected global::System.Web.UI.WebControls.TextBox txtUserPassword;
        protected global::System.Web.UI.WebControls.Label lblLoginMessage;
        protected global::System.Web.UI.WebControls.Button btnLogin;
        
        protected global::System.Web.UI.WebControls.Panel pnlUserDashboard;
        protected global::System.Web.UI.WebControls.Literal litMemberName;
        protected global::System.Web.UI.WebControls.Literal litMemberStatus;
        protected global::System.Web.UI.WebControls.Button btnLogout;
        
        protected global::System.Web.UI.WebControls.TextBox txtUserPhotoTitle;
        protected global::System.Web.UI.WebControls.TextBox txtUserPhotoDescription;
        protected global::System.Web.UI.WebControls.FileUpload fileUserPhoto;
        protected global::System.Web.UI.WebControls.Button btnUserUploadPhoto;
        protected global::System.Web.UI.WebControls.Label lblGalleryMsg;
        
        protected global::System.Web.UI.WebControls.Repeater rptUserPhotos;
        protected global::System.Web.UI.WebControls.Label lblNoPhotos;
        
        protected global::System.Web.UI.WebControls.Repeater rptUserBookings;
        protected global::System.Web.UI.WebControls.Label lblNoBookings;
        
        protected global::System.Web.UI.WebControls.Panel pnlUserReviewForm;
        protected global::System.Web.UI.WebControls.TextBox txtReviewEvent;
        protected global::System.Web.UI.WebControls.TextBox txtReviewTitle;
        protected global::System.Web.UI.WebControls.TextBox txtReviewText;
        protected global::System.Web.UI.WebControls.HiddenField hdnReviewRating;
        protected global::System.Web.UI.WebControls.Label lblReviewMsg;
        protected global::System.Web.UI.WebControls.Button btnSubmitReview;
        
        protected global::System.Web.UI.WebControls.Repeater rptUserReviews;
        protected global::System.Web.UI.WebControls.Label lblNoReviews;

        // Tab state
        protected global::System.Web.UI.WebControls.HiddenField hdnActiveUserTab;
    }
}

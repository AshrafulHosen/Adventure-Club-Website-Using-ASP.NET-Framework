using System;
using System.IO;
using AdventureHorizonsBD.BLL;
using AdventureHorizonsBD.DAL;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLoggedIn = IsUserLoggedIn();

            // Always restore panel visibility (works on postbacks too)
            pnlUserLoginSection.Visible = !isLoggedIn;
            pnlUserDashboard.Visible    =  isLoggedIn;

            // Only load data on initial page load AND only when authenticated.
            // Never call LoadDashboardData() when not logged in — Session values
            // will be null and cause NullReferenceException.
            if (!IsPostBack && isLoggedIn)
            {
                LoadDashboardData();
            }
        }

        private bool IsUserLoggedIn()
        {
            return Session["UserID"] != null
                && Session["UserRole"] != null
                && Session["UserRole"].ToString() == "Member";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblLoginMessage.Text = "";
            string email    = txtUserEmail.Text.Trim();
            string password = txtUserPassword.Text;

            AuthBLL authBLL = new AuthBLL();
            string msg;
            var user = authBLL.Login(email, password, out msg);

            if (user != null)
            {
                Session["UserID"]           = user.UserID;
                Session["UserName"]         = user.FullName;
                Session["UserRole"]         = user.Role;
                Session["MembershipPlan"]   = user.MembershipPlan;
                Session["RegistrationDate"] = user.RegistrationDate;

                Response.Redirect("User.aspx");
            }
            else
            {
                lblLoginMessage.Text = msg;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("User.aspx");
        }

        private void LoadDashboardData()
        {
            // Member info
            litMemberName.Text = Session["UserName"].ToString();
            DateTime regDate   = Convert.ToDateTime(Session["RegistrationDate"]);
            string plan        = Session["MembershipPlan"] != null
                                     ? Session["MembershipPlan"].ToString()
                                     : "Standard";
            litMemberStatus.Text = string.Format("Member since {0:MMM yyyy} | Plan: {1}", regDate, plan);

            int userId = Convert.ToInt32(Session["UserID"]);

            // Photos
            GalleryDAL galleryDAL = new GalleryDAL();
            var photos = galleryDAL.GetUserImages(userId);
            if (photos.Count > 0)
            {
                rptUserPhotos.DataSource = photos;
                rptUserPhotos.DataBind();
                rptUserPhotos.Visible = true;
                lblNoPhotos.Visible   = false;
            }
            else
            {
                rptUserPhotos.Visible = false;
                lblNoPhotos.Visible   = true;
            }

            // Bookings
            EventDAL eventDAL = new EventDAL();
            var bookings = eventDAL.GetUserRegistrations(userId);
            if (bookings.Count > 0)
            {
                rptUserBookings.DataSource = bookings;
                rptUserBookings.DataBind();
                rptUserBookings.Visible = true;
                lblNoBookings.Visible   = false;
            }
            else
            {
                rptUserBookings.Visible = false;
                lblNoBookings.Visible   = true;
            }

            // Reviews
            ReviewDAL reviewDAL = new ReviewDAL();
            var reviews = reviewDAL.GetUserReviews(userId);
            if (reviews.Count > 0)
            {
                rptUserReviews.DataSource = reviews;
                rptUserReviews.DataBind();
                rptUserReviews.Visible = true;
                lblNoReviews.Visible   = false;
            }
            else
            {
                rptUserReviews.Visible = false;
                lblNoReviews.Visible   = true;
            }
        }

        protected void btnUserUploadPhoto_Click(object sender, EventArgs e)
        {
            lblGalleryMsg.Text = "";

            if (!fileUserPhoto.HasFile)
            {
                lblGalleryMsg.ForeColor = System.Drawing.Color.Red;
                lblGalleryMsg.Text      = "Please select a file to upload.";
                return;
            }

            // Validate file type
            string ext = Path.GetExtension(fileUserPhoto.FileName).ToLower();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".gif" && ext != ".webp")
            {
                lblGalleryMsg.ForeColor = System.Drawing.Color.Red;
                lblGalleryMsg.Text      = "Only image files (jpg, jpeg, png, gif, webp) are allowed.";
                return;
            }

            try
            {
                string dir = Server.MapPath("~/images/gallery/");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string uniqueFilename = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileUserPhoto.FileName);
                string savePath       = Path.Combine(dir, uniqueFilename);
                fileUserPhoto.SaveAs(savePath);

                GalleryModel model = new GalleryModel
                {
                    Title            = txtUserPhotoTitle.Text.Trim(),
                    Description      = txtUserPhotoDescription.Text.Trim(),
                    ImageURL         = "images/gallery/" + uniqueFilename,
                    UploadedByUserID = Convert.ToInt32(Session["UserID"]),
                    IsApproved       = false   // requires admin approval
                };

                new GalleryDAL().AddImage(model);

                lblGalleryMsg.ForeColor      = System.Drawing.Color.Green;
                lblGalleryMsg.Text           = "Photo uploaded successfully! It is pending admin approval.";
                txtUserPhotoTitle.Text       = "";
                txtUserPhotoDescription.Text = "";

                // Refresh the photo list
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblGalleryMsg.ForeColor = System.Drawing.Color.Red;
                lblGalleryMsg.Text      = "Error uploading photo: " + ex.Message;
            }
        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            lblReviewMsg.Text = "";
            try
            {
                ReviewModel review = new ReviewModel
                {
                    UserID     = Convert.ToInt32(Session["UserID"]),
                    EventName  = txtReviewEvent.Text.Trim(),
                    Title      = txtReviewTitle.Text.Trim(),
                    ReviewText = txtReviewText.Text.Trim(),
                    Rating     = Convert.ToInt32(hdnReviewRating.Value)
                };

                new ReviewDAL().AddReview(review);

                lblReviewMsg.ForeColor  = System.Drawing.Color.Green;
                lblReviewMsg.Text       = "Review submitted successfully! It is pending admin approval.";
                txtReviewEvent.Text     = "";
                txtReviewTitle.Text     = "";
                txtReviewText.Text      = "";
                hdnReviewRating.Value   = "5";

                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblReviewMsg.ForeColor = System.Drawing.Color.Red;
                lblReviewMsg.Text      = "Error submitting review: " + ex.Message;
            }
        }
    }
}

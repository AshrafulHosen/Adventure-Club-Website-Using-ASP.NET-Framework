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
            if (!IsPostBack)
            {
                CheckLoginStatus();
            }
        }

        private void CheckLoginStatus()
        {
            if (Session["UserID"] != null && Session["UserRole"] != null && Session["UserRole"].ToString() == "Member")
            {
                pnlUserLoginSection.Visible = false;
                pnlUserDashboard.Visible = true;
                
                LoadDashboardData();
            }
            else
            {
                pnlUserLoginSection.Visible = true;
                pnlUserDashboard.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblLoginMessage.Text = "";
            string email = txtUserEmail.Text.Trim();
            string password = txtUserPassword.Text;

            AuthBLL authBLL = new AuthBLL();
            string msg;
            var user = authBLL.Login(email, password, out msg);

            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.FullName;
                Session["UserRole"] = user.Role;
                Session["MembershipPlan"] = user.MembershipPlan;
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
            // Load Member Info
            litMemberName.Text = Session["UserName"].ToString();
            DateTime regDate = Convert.ToDateTime(Session["RegistrationDate"]);
            string plan = Session["MembershipPlan"] != null ? Session["MembershipPlan"].ToString() : "Standard";
            litMemberStatus.Text = string.Format("Member since {0:MMM yyyy} | Plan: {1}", regDate, plan);

            int userId = Convert.ToInt32(Session["UserID"]);

            // Load Photos
            GalleryDAL galleryDAL = new GalleryDAL();
            var photos = galleryDAL.GetUserImages(userId);
            
            if (photos.Count > 0)
            {
                rptUserPhotos.DataSource = photos;
                rptUserPhotos.DataBind();
                rptUserPhotos.Visible = true;
                lblNoPhotos.Visible = false;
            }
            else
            {
                rptUserPhotos.Visible = false;
                lblNoPhotos.Visible = true;
            }

            // Load Bookings
            EventDAL eventDAL = new EventDAL();
            var bookings = eventDAL.GetUserRegistrations(userId);

            if (bookings.Count > 0)
            {
                rptUserBookings.DataSource = bookings;
                rptUserBookings.DataBind();
                rptUserBookings.Visible = true;
                lblNoBookings.Visible = false;
            }
            else
            {
                rptUserBookings.Visible = false;
                lblNoBookings.Visible = true;
            }

            // Load Reviews
            ReviewDAL reviewDAL = new ReviewDAL();
            var reviews = reviewDAL.GetUserReviews(userId);
            if (reviews.Count > 0)
            {
                rptUserReviews.DataSource = reviews;
                rptUserReviews.DataBind();
                rptUserReviews.Visible = true;
                lblNoReviews.Visible = false;
            }
            else
            {
                rptUserReviews.Visible = false;
                lblNoReviews.Visible = true;
            }
        }

        protected void btnUserUploadPhoto_Click(object sender, EventArgs e)
        {
            lblGalleryMsg.Text = "";
            
            if (!fileUserPhoto.HasFile)
            {
                lblGalleryMsg.ForeColor = System.Drawing.Color.Red;
                lblGalleryMsg.Text = "Please select a file to upload.";
                return;
            }

            try
            {
                string filename = Path.GetFileName(fileUserPhoto.FileName);
                // In a real app we'd use a Guid to prevent overwriting
                string uniqueFilename = Guid.NewGuid().ToString() + "_" + filename;
                
                string savePath = Server.MapPath("~/images/gallery/") + uniqueFilename;
                
                // Ensure directory exists
                string dir = Server.MapPath("~/images/gallery/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                fileUserPhoto.SaveAs(savePath);

                GalleryModel model = new GalleryModel
                {
                    Title = txtUserPhotoTitle.Text.Trim(),
                    Description = txtUserPhotoDescription.Text.Trim(),
                    ImageURL = "images/gallery/" + uniqueFilename,
                    UploadedByUserID = Convert.ToInt32(Session["UserID"]),
                    IsApproved = false // Requires admin approval
                };

                GalleryDAL dal = new GalleryDAL();
                dal.AddImage(model);

                lblGalleryMsg.ForeColor = System.Drawing.Color.Green;
                lblGalleryMsg.Text = "Photo uploaded successfully! It is pending admin approval.";
                
                txtUserPhotoTitle.Text = "";
                txtUserPhotoDescription.Text = "";
                
                // Refresh list
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblGalleryMsg.ForeColor = System.Drawing.Color.Red;
                lblGalleryMsg.Text = "Error uploading photo: " + ex.Message;
            }
        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            lblReviewMsg.Text = "";
            try
            {
                ReviewModel review = new ReviewModel
                {
                    UserID = Convert.ToInt32(Session["UserID"]),
                    EventName = txtReviewEvent.Text.Trim(),
                    Title = txtReviewTitle.Text.Trim(),
                    ReviewText = txtReviewText.Text.Trim(),
                    Rating = Convert.ToInt32(hdnReviewRating.Value)
                };

                ReviewDAL dal = new ReviewDAL();
                dal.AddReview(review);

                lblReviewMsg.ForeColor = System.Drawing.Color.Green;
                lblReviewMsg.Text = "Review submitted successfully! It is pending admin approval.";

                txtReviewEvent.Text = "";
                txtReviewTitle.Text = "";
                txtReviewText.Text = "";
                hdnReviewRating.Value = "5";

                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblReviewMsg.ForeColor = System.Drawing.Color.Red;
                lblReviewMsg.Text = "Error submitting review: " + ex.Message;
            }
        }
    }
}
using System;
using System.IO;
using System.Web.UI.WebControls;
using AdventureHorizonsBD.BLL;
using AdventureHorizonsBD.DAL;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD
{
    public partial class Admin : System.Web.UI.Page
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
            if (Session["UserID"] != null && Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                pnlAdminLoginSection.Visible = false;
                pnlAdminDashboard.Visible = true;
                
                LoadDashboardData();
            }
            else
            {
                pnlAdminLoginSection.Visible = true;
                pnlAdminDashboard.Visible = false;
            }
        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            lblAdminLoginMsg.Text = "";
            string email = txtAdminEmail.Text.Trim();
            string password = txtAdminPassword.Text;

            AuthBLL authBLL = new AuthBLL();
            string msg;
            var user = authBLL.Login(email, password, out msg);

            if (user != null && user.Role == "Admin")
            {
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.FullName;
                Session["UserRole"] = user.Role;
                
                Response.Redirect("Admin.aspx");
            }
            else
            {
                lblAdminLoginMsg.Text = user != null ? "Access denied. Admin rights required." : msg;
            }
        }

        protected void btnAdminLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Admin.aspx");
        }

        private void LoadDashboardData()
        {
            // Load Stats
            DashboardDAL dashDAL = new DashboardDAL();
            var stats = dashDAL.GetDashboardStats();
            
            if(stats.Count > 0)
            {
                litStatMembers.Text = stats["ApprovedMembers"].ToString();
                litStatPending.Text = stats["PendingRequests"].ToString();
                litStatEvents.Text = stats["TotalEvents"].ToString();
                litStatMessages.Text = stats["UnreadMessages"].ToString();
            }

            // Load Membership Requests
            MembershipDAL memDAL = new MembershipDAL();
            var requests = memDAL.GetPendingRequests();
            if(requests.Count > 0)
            {
                rptMembershipRequests.DataSource = requests;
                rptMembershipRequests.DataBind();
                rptMembershipRequests.Visible = true;
                rowNoRequests.Visible = false;
            }
            else
            {
                rptMembershipRequests.Visible = false;
                rowNoRequests.Visible = true;
            }

            // Load Approved Members
            UserDAL userDAL = new UserDAL();
            var approvedMembers = userDAL.GetApprovedMembers();
            if(approvedMembers.Count > 0)
            {
                rptApprovedMembers.DataSource = approvedMembers;
                rptApprovedMembers.DataBind();
                rptApprovedMembers.Visible = true;
                lblNoApprovedMembers.Visible = false;
            }
            else
            {
                rptApprovedMembers.Visible = false;
                lblNoApprovedMembers.Visible = true;
            }

            // Load Events
            EventDAL eventDAL = new EventDAL();
            var events = eventDAL.GetAllEvents();
            if (events.Count > 0)
            {
                rptAdminEvents.DataSource = events;
                rptAdminEvents.DataBind();
                rptAdminEvents.Visible = true;
                lblNoEvents.Visible = false;
            }
            else
            {
                rptAdminEvents.Visible = false;
                lblNoEvents.Visible = true;
            }

            // Load Gallery
            GalleryDAL galleryDAL = new GalleryDAL();
            var images = galleryDAL.GetAllImages();
            if (images.Count > 0)
            {
                rptAdminGallery.DataSource = images;
                rptAdminGallery.DataBind();
                rptAdminGallery.Visible = true;
                lblNoGallery.Visible = false;
            }
            else
            {
                rptAdminGallery.Visible = false;
                lblNoGallery.Visible = true;
            }

            // Load Contact Messages
            ContactDAL contactDAL = new ContactDAL();
            var msgs = contactDAL.GetAllMessages();
            rptContactMessages.DataSource = msgs;
            rptContactMessages.DataBind();

            // Load Bookings
            var bookings = eventDAL.GetAllRegistrations();
            if(bookings.Count > 0)
            {
                rptAdminBookings.DataSource = bookings;
                rptAdminBookings.DataBind();
                rptAdminBookings.Visible = true;
                lblNoBookings2.Visible = false;
            }
            else
            {
                rptAdminBookings.Visible = false;
                lblNoBookings2.Visible = true;
            }

            // Load Reviews
            ReviewDAL reviewDAL = new ReviewDAL();
            var reviews = reviewDAL.GetAllReviews();
            if (reviews.Count > 0)
            {
                rptAdminReviews.DataSource = reviews;
                rptAdminReviews.DataBind();
                rptAdminReviews.Visible = true;
                lblNoReviews.Visible = false;
            }
            else
            {
                rptAdminReviews.Visible = false;
                lblNoReviews.Visible = true;
            }
        }

        protected void btnAdminAddEvent_Click(object sender, EventArgs e)
        {
            lblAdminEventMsg.Text = "";
            try
            {
                EventModel evt = new EventModel
                {
                    Title = txtAdminEventTitle.Text.Trim(),
                    EventDate = txtAdminEventDate.Text.Trim(),
                    EventDuration = txtAdminEventDuration.Text.Trim(),
                    Region = txtAdminEventRegion.Text.Trim(),
                    Description = txtAdminEventDescription.Text.Trim()
                };

                EventDAL eventDAL = new EventDAL();
                eventDAL.AddEvent(evt);

                lblAdminEventMsg.ForeColor = System.Drawing.Color.Green;
                lblAdminEventMsg.Text = "Event added successfully!";

                // Clear form
                txtAdminEventTitle.Text = "";
                txtAdminEventDate.Text = "";
                txtAdminEventDuration.Text = "";
                txtAdminEventRegion.Text = "";
                txtAdminEventDescription.Text = "";

                LoadDashboardData();
            }
            catch(Exception ex)
            {
                lblAdminEventMsg.ForeColor = System.Drawing.Color.Red;
                lblAdminEventMsg.Text = "Error adding event: " + ex.Message;
            }
        }

        protected void rptAdminEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int eventId = Convert.ToInt32(e.CommandArgument);
                EventDAL eventDAL = new EventDAL();
                eventDAL.DeleteEvent(eventId);

                LoadDashboardData();
            }
        }

        protected void btnAdminAddPhoto_Click(object sender, EventArgs e)
        {
            lblAdminGalleryMsg.Text = "";
            try
            {
                string imageUrl = "";

                // Check which upload method is being used
                if (fileAdminPhoto.HasFile)
                {
                    string filename = Path.GetFileName(fileAdminPhoto.FileName);
                    string uniqueFilename = Guid.NewGuid().ToString() + "_" + filename;
                    string savePath = Server.MapPath("~/images/gallery/") + uniqueFilename;
                    
                    // Ensure directory exists
                    string dir = Server.MapPath("~/images/gallery/");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    
                    fileAdminPhoto.SaveAs(savePath);
                    imageUrl = "images/gallery/" + uniqueFilename;
                }
                else if (!string.IsNullOrEmpty(txtAdminPhotoUrl.Text.Trim()))
                {
                    imageUrl = txtAdminPhotoUrl.Text.Trim();
                }
                else
                {
                    lblAdminGalleryMsg.ForeColor = System.Drawing.Color.Red;
                    lblAdminGalleryMsg.Text = "Please either upload a file or provide an image URL.";
                    return;
                }

                GalleryModel model = new GalleryModel
                {
                    Title = txtAdminPhotoTitle.Text.Trim(),
                    Description = txtAdminPhotoDescription.Text.Trim(),
                    ImageURL = imageUrl,
                    UploadedByUserID = Convert.ToInt32(Session["UserID"]),
                    IsApproved = true // Admin uploads are auto-approved
                };

                GalleryDAL dal = new GalleryDAL();
                dal.AddImage(model);

                lblAdminGalleryMsg.ForeColor = System.Drawing.Color.Green;
                lblAdminGalleryMsg.Text = "Photo added successfully!";

                // Clear form
                txtAdminPhotoTitle.Text = "";
                txtAdminPhotoUrl.Text = "";
                txtAdminPhotoDescription.Text = "";

                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblAdminGalleryMsg.ForeColor = System.Drawing.Color.Red;
                lblAdminGalleryMsg.Text = "Error uploading photo: " + ex.Message;
            }
        }

        protected void rptMembershipRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int requestId = Convert.ToInt32(e.CommandArgument);
            MembershipBLL memBLL = new MembershipBLL();
            
            if (e.CommandName == "Approve")
            {
                memBLL.ApproveRequest(requestId, out _);
            }
            else if (e.CommandName == "Reject")
            {
                memBLL.RejectRequest(requestId, out _);
            }
            
            LoadDashboardData(); // Refresh data
        }

        protected void rptContactMessages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkRead")
            {
                int messageId = Convert.ToInt32(e.CommandArgument);
                ContactDAL contactDAL = new ContactDAL();
                contactDAL.MarkAsRead(messageId);
                
                LoadDashboardData();
            }
        }
        
        protected void rptAdminGallery_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int imageId = Convert.ToInt32(e.CommandArgument);
            GalleryDAL galleryDAL = new GalleryDAL();
            
            if(e.CommandName == "Approve")
            {
                galleryDAL.ApproveImage(imageId);
            }
            else if(e.CommandName == "Delete")
            {
                galleryDAL.DeleteImage(imageId);
            }
            
            LoadDashboardData();
        }

        protected void rptAdminBookings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int regId = Convert.ToInt32(e.CommandArgument);
            EventDAL eventDAL = new EventDAL();

            if (e.CommandName == "Approve")
            {
                eventDAL.UpdateRegistrationStatus(regId, "Approved");
            }
            else if (e.CommandName == "Reject")
            {
                eventDAL.UpdateRegistrationStatus(regId, "Rejected");
            }

            LoadDashboardData();
        }
 
        protected void rptAdminReviews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int reviewId = Convert.ToInt32(e.CommandArgument);
            ReviewDAL reviewDAL = new ReviewDAL();

            if (e.CommandName == "Approve")
            {
                reviewDAL.UpdateReviewStatus(reviewId, true);
            }
            else if (e.CommandName == "Reject")
            {
                reviewDAL.UpdateReviewStatus(reviewId, false);
            }
            else if (e.CommandName == "Delete")
            {
                reviewDAL.DeleteReview(reviewId);
            }

            LoadDashboardData();
        }
    }
}
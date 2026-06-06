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
            bool isAdmin = Session["UserID"] != null
                        && Session["UserRole"] != null
                        && Session["UserRole"].ToString() == "Admin";

            pnlAdminLoginSection.Visible = !isAdmin;
            pnlAdminDashboard.Visible    =  isAdmin;

            if (!IsPostBack && isAdmin)
            {
                LoadDashboardData();
            }
        }

        // Restores panel visibility without touching any data-bound controls.
        private void RestoreLoginState()
        {
            bool isAdmin = Session["UserID"] != null
                        && Session["UserRole"] != null
                        && Session["UserRole"].ToString() == "Admin";

            pnlAdminLoginSection.Visible = !isAdmin;
            pnlAdminDashboard.Visible    =  isAdmin;
        }

        private void CheckLoginStatus()
        {
            RestoreLoginState();
            if (pnlAdminDashboard.Visible)
                LoadDashboardData();
        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            lblAdminLoginMsg.Text = "";
            AuthBLL authBLL = new AuthBLL();
            string msg;
            var user = authBLL.Login(txtAdminEmail.Text.Trim(), txtAdminPassword.Text, out msg);

            if (user != null && user.Role == "Admin")
            {
                Session["UserID"]   = user.UserID;
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
            // Stats
            DashboardDAL dashDAL = new DashboardDAL();
            var stats = dashDAL.GetDashboardStats();
            if (stats.Count > 0)
            {
                litStatMembers.Text  = stats["ApprovedMembers"].ToString();
                litStatPending.Text  = stats["PendingRequests"].ToString();
                litStatEvents.Text   = stats["TotalEvents"].ToString();
                litStatMessages.Text = stats["UnreadMessages"].ToString();
            }

            // Membership requests
            MembershipDAL memDAL  = new MembershipDAL();
            var requests = memDAL.GetPendingRequests();
            if (requests.Count > 0)
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

            // Approved members
            UserDAL userDAL = new UserDAL();
            var approvedMembers = userDAL.GetApprovedMembers();
            if (approvedMembers.Count > 0)
            {
                rptApprovedMembers.DataSource = approvedMembers;
                rptApprovedMembers.DataBind();
                rptApprovedMembers.Visible    = true;
                lblNoApprovedMembers.Visible  = false;
            }
            else
            {
                rptApprovedMembers.Visible   = false;
                lblNoApprovedMembers.Visible = true;
            }

            // Events
            EventDAL eventDAL = new EventDAL();
            var events = eventDAL.GetAllEvents();
            if (events.Count > 0)
            {
                rptAdminEvents.DataSource = events;
                rptAdminEvents.DataBind();
                rptAdminEvents.Visible = true;
                lblNoEvents.Visible    = false;
            }
            else
            {
                rptAdminEvents.Visible = false;
                lblNoEvents.Visible    = true;
            }

            // Gallery
            GalleryDAL galleryDAL = new GalleryDAL();
            var images = galleryDAL.GetAllImages();
            if (images.Count > 0)
            {
                rptAdminGallery.DataSource = images;
                rptAdminGallery.DataBind();
                rptAdminGallery.Visible = true;
                lblNoGallery.Visible    = false;
            }
            else
            {
                rptAdminGallery.Visible = false;
                lblNoGallery.Visible    = true;
            }

            // Contact messages
            ContactDAL contactDAL = new ContactDAL();
            var msgs = contactDAL.GetAllMessages();
            rptContactMessages.DataSource = msgs;
            rptContactMessages.DataBind();

            // Bookings
            var bookings = eventDAL.GetAllRegistrations();
            if (bookings.Count > 0)
            {
                rptAdminBookings.DataSource = bookings;
                rptAdminBookings.DataBind();
                rptAdminBookings.Visible = true;
                lblNoBookings2.Visible   = false;
            }
            else
            {
                rptAdminBookings.Visible = false;
                lblNoBookings2.Visible   = true;
            }

            // Reviews
            ReviewDAL reviewDAL = new ReviewDAL();
            var reviews = reviewDAL.GetAllReviews();
            if (reviews.Count > 0)
            {
                rptAdminReviews.DataSource = reviews;
                rptAdminReviews.DataBind();
                rptAdminReviews.Visible  = true;
                lblNoAdminReviews.Visible = false;
            }
            else
            {
                rptAdminReviews.Visible  = false;
                lblNoAdminReviews.Visible = true;
            }
        }

        // ── Events ───────────────────────────────────────────────────────────

        protected void btnAdminAddEvent_Click(object sender, EventArgs e)
        {
            lblAdminEventMsg.Text = "";
            try
            {
                var evt = new EventModel
                {
                    Title         = txtAdminEventTitle.Text.Trim(),
                    EventDate     = txtAdminEventDate.Text.Trim(),
                    EventDuration = txtAdminEventDuration.Text.Trim(),
                    Region        = txtAdminEventRegion.Text.Trim(),
                    Description   = txtAdminEventDescription.Text.Trim()
                };
                new EventDAL().AddEvent(evt);

                lblAdminEventMsg.ForeColor = System.Drawing.Color.Green;
                lblAdminEventMsg.Text      = "Event added successfully!";
                txtAdminEventTitle.Text = txtAdminEventDate.Text =
                    txtAdminEventDuration.Text = txtAdminEventRegion.Text =
                    txtAdminEventDescription.Text = "";
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblAdminEventMsg.ForeColor = System.Drawing.Color.Red;
                lblAdminEventMsg.Text      = "Error adding event: " + ex.Message;
            }
        }

        protected void rptAdminEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int eventId = Convert.ToInt32(e.CommandArgument);
            EventDAL eventDAL = new EventDAL();

            if (e.CommandName == "Delete")
            {
                eventDAL.DeleteEvent(eventId);
                pnlEditEvent.Visible = false;
                LoadDashboardData();
            }
            else if (e.CommandName == "EditEvent")
            {
                // Load the data first so the edit panel is populated,
                // then show the panel (order matters for visibility).
                EventModel evt = eventDAL.GetEventById(eventId);
                if (evt != null)
                {
                    hdnEditEventID.Value         = eventId.ToString();
                    txtEditEventTitle.Text       = evt.Title;
                    txtEditEventDate.Text        = evt.EventDate;
                    txtEditEventDuration.Text    = evt.EventDuration;
                    txtEditEventRegion.Text      = evt.Region;
                    txtEditEventDescription.Text = evt.Description;
                    lblEditEventMsg.Text         = "";
                    pnlEditEvent.Visible         = true;
                    hdnActiveAdminTab.Value = "events";

                    // Refresh the repeater list AFTER setting the panel visible
                    // so the list is up-to-date but the edit form stays open.
                    LoadDashboardData();
                }
            }
        }

        protected void btnSaveEvent_Click(object sender, EventArgs e)
        {
            lblEditEventMsg.Text = "";
            try
            {
                new EventDAL().UpdateEvent(new EventModel
                {
                    EventID       = Convert.ToInt32(hdnEditEventID.Value),
                    Title         = txtEditEventTitle.Text.Trim(),
                    EventDate     = txtEditEventDate.Text.Trim(),
                    EventDuration = txtEditEventDuration.Text.Trim(),
                    Region        = txtEditEventRegion.Text.Trim(),
                    Description   = txtEditEventDescription.Text.Trim()
                });

                lblAdminEventMsg.ForeColor = System.Drawing.Color.Green;
                lblAdminEventMsg.Text      = "Event updated successfully!";
                pnlEditEvent.Visible       = false;
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblEditEventMsg.ForeColor = System.Drawing.Color.Red;
                lblEditEventMsg.Text      = "Error saving event: " + ex.Message;
            }
        }

        protected void btnCancelEditEvent_Click(object sender, EventArgs e)
        {
            pnlEditEvent.Visible = false;
            LoadDashboardData();
        }

        // ── Gallery ──────────────────────────────────────────────────────────

        protected void btnAdminAddPhoto_Click(object sender, EventArgs e)
        {
            lblAdminGalleryMsg.Text = "";
            try
            {
                string imageUrl = "";
                if (fileAdminPhoto.HasFile)
                {
                    string dir = Server.MapPath("~/images/gallery/");
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    string unique = Guid.NewGuid() + "_" + Path.GetFileName(fileAdminPhoto.FileName);
                    fileAdminPhoto.SaveAs(dir + unique);
                    imageUrl = "images/gallery/" + unique;
                }
                else if (!string.IsNullOrWhiteSpace(txtAdminPhotoUrl.Text))
                {
                    imageUrl = txtAdminPhotoUrl.Text.Trim();
                }
                else
                {
                    lblAdminGalleryMsg.ForeColor = System.Drawing.Color.Red;
                    lblAdminGalleryMsg.Text      = "Please upload a file or provide an image URL.";
                    return;
                }

                new GalleryDAL().AddImage(new GalleryModel
                {
                    Title            = txtAdminPhotoTitle.Text.Trim(),
                    Description      = txtAdminPhotoDescription.Text.Trim(),
                    ImageURL         = imageUrl,
                    UploadedByUserID = Convert.ToInt32(Session["UserID"]),
                    IsApproved       = true
                });

                lblAdminGalleryMsg.ForeColor = System.Drawing.Color.Green;
                lblAdminGalleryMsg.Text      = "Photo added successfully!";
                txtAdminPhotoTitle.Text = txtAdminPhotoUrl.Text = txtAdminPhotoDescription.Text = "";
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblAdminGalleryMsg.ForeColor = System.Drawing.Color.Red;
                lblAdminGalleryMsg.Text      = "Error uploading photo: " + ex.Message;
            }
        }

        protected void rptAdminGallery_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int imageId = Convert.ToInt32(e.CommandArgument);
            GalleryDAL galleryDAL = new GalleryDAL();

            if (e.CommandName == "Approve")
            {
                galleryDAL.ApproveImage(imageId);
                pnlEditImage.Visible = false;
                LoadDashboardData();
            }
            else if (e.CommandName == "Delete")
            {
                galleryDAL.DeleteImage(imageId);
                pnlEditImage.Visible = false;
                LoadDashboardData();
            }
            else if (e.CommandName == "EditImage")
            {
                GalleryModel img = galleryDAL.GetImageById(imageId);
                if (img != null)
                {
                    hdnEditImageID.Value         = imageId.ToString();
                    hdnEditImageURL.Value        = img.ImageURL;          // keep current URL
                    txtEditImageTitle.Text       = img.Title;
                    txtEditImageDescription.Text = img.Description;
                    imgEditPreview.ImageUrl      = "~/" + img.ImageURL;   // show current photo
                    imgEditPreview.Visible       = !string.IsNullOrEmpty(img.ImageURL);
                    lblEditImageMsg.Text         = "";
                    pnlEditImage.Visible         = true;

                    // Refresh list AFTER setting panel visible
                    LoadDashboardData();
                }
            }
        }

        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            lblEditImageMsg.Text = "";
            try
            {
                // Determine the image URL — replace only if a new file was uploaded
                string imageUrl = hdnEditImageURL.Value;  // keep existing by default

                if (fileEditPhoto.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(fileEditPhoto.FileName).ToLower();
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".gif" && ext != ".webp")
                    {
                        lblEditImageMsg.ForeColor = System.Drawing.Color.Red;
                        lblEditImageMsg.Text      = "Only image files (jpg, jpeg, png, gif, webp) are allowed.";
                        return;
                    }

                    string dir = Server.MapPath("~/images/gallery/");
                    if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
                    string unique = Guid.NewGuid() + "_" + System.IO.Path.GetFileName(fileEditPhoto.FileName);
                    fileEditPhoto.SaveAs(System.IO.Path.Combine(dir, unique));
                    imageUrl = "images/gallery/" + unique;
                }

                new GalleryDAL().UpdateImage(new GalleryModel
                {
                    ImageID     = Convert.ToInt32(hdnEditImageID.Value),
                    Title       = txtEditImageTitle.Text.Trim(),
                    Description = txtEditImageDescription.Text.Trim(),
                    ImageURL    = imageUrl   // existing URL kept unless new file uploaded
                });

                lblAdminGalleryMsg.ForeColor = System.Drawing.Color.Green;
                lblAdminGalleryMsg.Text      = "Photo updated successfully!";
                pnlEditImage.Visible         = false;
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                lblEditImageMsg.ForeColor = System.Drawing.Color.Red;
                lblEditImageMsg.Text      = "Error saving photo: " + ex.Message;
            }
        }

        protected void btnCancelEditImage_Click(object sender, EventArgs e)
        {
            pnlEditImage.Visible = false;
            LoadDashboardData();
        }

        // ── Membership ───────────────────────────────────────────────────────

        protected void rptMembershipRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int requestId = Convert.ToInt32(e.CommandArgument);
            MembershipBLL memBLL = new MembershipBLL();
            if (e.CommandName == "Approve") memBLL.ApproveRequest(requestId, out _);
            else if (e.CommandName == "Reject") memBLL.RejectRequest(requestId, out _);
            LoadDashboardData();
        }

        // ── Messages ─────────────────────────────────────────────────────────

        protected void rptContactMessages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkRead")
            {
                new ContactDAL().MarkAsRead(Convert.ToInt32(e.CommandArgument));
                LoadDashboardData();
            }
        }

        // ── Bookings ─────────────────────────────────────────────────────────

        protected void rptAdminBookings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int regId = Convert.ToInt32(e.CommandArgument);
            EventDAL eventDAL = new EventDAL();
            if (e.CommandName == "Approve")      eventDAL.UpdateRegistrationStatus(regId, "Approved");
            else if (e.CommandName == "Reject")  eventDAL.UpdateRegistrationStatus(regId, "Rejected");
            LoadDashboardData();
        }

        // ── Reviews ──────────────────────────────────────────────────────────

        protected void rptAdminReviews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int reviewId = Convert.ToInt32(e.CommandArgument);
            ReviewDAL reviewDAL = new ReviewDAL();
            if (e.CommandName == "Approve")      reviewDAL.UpdateReviewStatus(reviewId, true);
            else if (e.CommandName == "Reject")  reviewDAL.UpdateReviewStatus(reviewId, false);
            else if (e.CommandName == "Delete")  reviewDAL.DeleteReview(reviewId);
            LoadDashboardData();
        }
    }
}

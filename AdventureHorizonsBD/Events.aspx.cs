using System;
using AdventureHorizonsBD.DAL;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD
{
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEvents();
            }
        }

        private void LoadEvents()
        {
            EventDAL eventDAL = new EventDAL();
            rptEvents.DataSource = eventDAL.GetAllEvents();
            rptEvents.DataBind();
        }

        protected void btnSubmitBooking_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblBookingMsg.ForeColor = System.Drawing.Color.Red;
                lblBookingMsg.Text = "You must be logged in to book an event. <a href='User.aspx'>Login here</a>";
                // Show modal again via JS
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", "document.getElementById('bookingModal').style.display='block';", true);
                return;
            }

            try
            {
                int eventId = Convert.ToInt32(hdnEventID.Value);
                int participants = Convert.ToInt32(txtBookingParticipants.Text);
                string requests = txtBookingRequests.Text.Trim();

                EventRegistrationModel reg = new EventRegistrationModel
                {
                    UserID = Convert.ToInt32(Session["UserID"]),
                    EventID = eventId,
                    NumberOfParticipants = participants,
                    SpecialRequests = requests
                };

                EventDAL eventDAL = new EventDAL();
                eventDAL.RegisterForEvent(reg);

                lblBookingMsg.ForeColor = System.Drawing.Color.Green;
                lblBookingMsg.Text = "Booking successful! View it in your dashboard.";
                
                txtBookingParticipants.Text = "1";
                txtBookingRequests.Text = "";
                
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", "document.getElementById('bookingModal').style.display='block';", true);
            }
            catch (Exception ex)
            {
                lblBookingMsg.ForeColor = System.Drawing.Color.Red;
                lblBookingMsg.Text = "Error submitting booking: " + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", "document.getElementById('bookingModal').style.display='block';", true);
            }
        }
    }
}
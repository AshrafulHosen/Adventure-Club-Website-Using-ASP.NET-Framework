using System;
using AdventureHorizonsBD.DAL;
using AdventureHorizonsBD.Models;

namespace AdventureHorizonsBD
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSendMsg_Click(object sender, EventArgs e)
        {
            try
            {
                ContactMessageModel msg = new ContactMessageModel
                {
                    Email = txtContactEmail.Text.Trim(),
                    Message = "From: " + txtContactName.Text.Trim() + "\nSubject: " + txtContactSubject.Text.Trim() + "\n\n" + txtContactMessage.Text.Trim()
                };

                ContactDAL contactDAL = new ContactDAL();
                contactDAL.SubmitMessage(msg);

                lblContactMessage.ForeColor = System.Drawing.Color.Green;
                lblContactMessage.Text = "Thank you! Your message has been sent successfully.";

                txtContactName.Text = "";
                txtContactEmail.Text = "";
                txtContactSubject.Text = "";
                txtContactMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblContactMessage.ForeColor = System.Drawing.Color.Red;
                lblContactMessage.Text = "Sorry, an error occurred while sending your message: " + ex.Message;
            }
        }
    }
}
using System;
using AdventureHorizonsBD.BLL;

namespace AdventureHorizonsBD
{
    public partial class Membership : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmitApp_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Red;

            string name = txtAppName.Text.Trim();
            string email = txtAppEmail.Text.Trim();
            string phone = txtAppPhone.Text.Trim();
            string password = txtAppPassword.Text;
            string plan = ddlAppPlan.SelectedValue;
            string experience = ddlAppExperience.SelectedValue;
            
            // Note: txtAppMessage is optional and we aren't storing it right now, 
            // but we could add it to Users/MembershipRequests if needed.

            AuthBLL authBLL = new AuthBLL();
            string msg;
            
            bool success = authBLL.Register(name, email, password, phone, plan, experience, out msg);
            
            if (success)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = msg;
                
                // Fetch the newly created user to create the membership request
                var user = new DAL.UserDAL().GetUserByEmail(email);
                if (user != null)
                {
                    MembershipBLL memBLL = new MembershipBLL();
                    memBLL.SubmitMembershipRequest(user.UserID, plan, out _);
                }

                // Clear form
                txtAppName.Text = "";
                txtAppEmail.Text = "";
                txtAppPhone.Text = "";
                txtAppMessage.Text = "";
                ddlAppPlan.SelectedIndex = 0;
                ddlAppExperience.SelectedIndex = 0;
            }
            else
            {
                lblMessage.Text = msg;
            }
        }
    }
}
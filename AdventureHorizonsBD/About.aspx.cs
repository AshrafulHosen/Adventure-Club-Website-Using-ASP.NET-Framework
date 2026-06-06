using System;
using AdventureHorizonsBD.DAL;

namespace AdventureHorizonsBD
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadApprovedReviews();
            }
        }

        private void LoadApprovedReviews()
        {
            var reviews = new ReviewDAL().GetApprovedReviews();
            if (reviews.Count > 0)
            {
                rptCommunityReviews.DataSource = reviews;
                rptCommunityReviews.DataBind();
                rptCommunityReviews.Visible = true;
                lblNoReviews.Visible        = false;
            }
            else
            {
                rptCommunityReviews.Visible = false;
                lblNoReviews.Visible        = true;
            }
        }
    }
}

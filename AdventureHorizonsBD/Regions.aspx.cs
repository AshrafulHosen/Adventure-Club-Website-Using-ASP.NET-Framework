using System;
using AdventureHorizonsBD.DAL;

namespace AdventureHorizonsBD
{
    public partial class Regions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRegions();
            }
        }

        private void LoadRegions()
        {
            RegionDAL regionDAL = new RegionDAL();
            rptRegions.DataSource = regionDAL.GetAllRegions();
            rptRegions.DataBind();
        }
    }
}
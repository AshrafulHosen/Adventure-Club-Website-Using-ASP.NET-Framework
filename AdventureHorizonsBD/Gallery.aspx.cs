using System;
using AdventureHorizonsBD.DAL;

namespace AdventureHorizonsBD
{
    public partial class Gallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGallery();
            }
        }

        private void LoadGallery()
        {
            GalleryDAL galleryDAL = new GalleryDAL();
            rptGallery.DataSource = galleryDAL.GetApprovedImages();
            rptGallery.DataBind();
        }
    }
}
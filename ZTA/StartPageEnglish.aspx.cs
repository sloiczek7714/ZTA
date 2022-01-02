using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class StartPageEnglish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void polishVersion(object sender, EventArgs e)
        {
            Response.Redirect("StartPage.aspx");
        }
        protected void GoToLoginPage(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }
    }
}
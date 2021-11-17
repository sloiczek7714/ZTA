using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class EditUserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Edit(object sender, EventArgs e)
        {

        }
        protected void GoToCheckListPage(object sender, EventArgs e)
        {
            Response.Redirect("CheckListPage.aspx");
        }

        protected void GoToReportPage(object sender, EventArgs e)
        {
            Response.Redirect("ReportPage.aspx");
        }
    }
}
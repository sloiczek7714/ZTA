using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class ListOfProcedures : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ID"] != null)
            {
                string ID = Session["ID"].ToString();
            }

            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void goToNewProcedure(object sender, EventArgs e)
        {
            Session["ID"] = ID;
            Response.Redirect("NewProcedurePage.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }
        protected void ZTA_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
       
    }
}
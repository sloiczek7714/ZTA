using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                string ID = Session["ID"].ToString();
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
                connection.Open();
                string insert = "Select * FROM Users where ID = @ID";
                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("ID", ID);
                SqlDataReader DataReader = command.ExecuteReader();
                if (DataReader.Read())
                {
                    EmailLabel.Text = DataReader.GetValue(1).ToString();
                    NameLabel.Text = DataReader.GetValue(2).ToString();
                    SurnameLabel.Text = DataReader.GetValue(4).ToString();
                    PositionLabel.Text = DataReader.GetValue(5).ToString();
                    WorkPlaceLabel.Text = DataReader.GetValue(6).ToString();
                    RoleLabel.Text = DataReader.GetValue(7).ToString();
                    SystemNameLabel.Text = DataReader.GetValue(8).ToString();
                }

            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
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
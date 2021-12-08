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
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                connection.Open();
                string insert = "SELECT Users.User_ID, Users.Name, Users.Surname, Users.Position, Users.WorkPlace, Users.Email, Users.Role, Users_Boss.Boss_ID FROM Users  LEFT JOIN Users_Boss ON Users.User_ID = Users_Boss.User_ID where Users.User_ID = @ID";
                SqlCommand command = new SqlCommand(insert, connection);
                command.Parameters.AddWithValue("ID", ID);
                SqlDataReader DataReader = command.ExecuteReader();
                if (DataReader.Read())
                {
                    NameLabel.Text = DataReader.GetValue(1).ToString();
                    SurnameLabel.Text = DataReader.GetValue(2).ToString();
                    PositionLabel.Text = DataReader.GetValue(3).ToString();
                    WorkPlaceLabel.Text = DataReader.GetValue(4).ToString();
                    EmailLabel.Text = DataReader.GetValue(5).ToString();
                    RoleLabel.Text = DataReader.GetValue(6).ToString();
                    BossIDLabel.Text = DataReader.GetValue(7).ToString();
                }

                if(Helper.DoesUserHasPermission(ID, "Administrator"))
                {
                    userButton.Visible = true;
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
            Response.Redirect("LoginPage.aspx");
        }
        protected void Edit(object sender, EventArgs e)
        {

        }
        protected void GoToCheckListPage(object sender, EventArgs e)
        {
            Response.Redirect("ListOfProcedures.aspx");
        }

        protected void GoToAdminPage(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }

        protected void GoToReportPage(object sender, EventArgs e)
        {
            Response.Redirect("ReportPage.aspx");
        }
    }
}
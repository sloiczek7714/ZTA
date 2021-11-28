using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace ZTA
{
    public partial class AdminPage : System.Web.UI.Page
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

        protected void GoToAddUserPage(object sender, EventArgs e)
        {
            Response.Redirect("AddUserPage.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }
        protected void EditUser(object sender, EventArgs e)
        {
            if (GridView.SelectedRow != null)
            {
                GridView gridView = (GridView)this.Page.FindControl("GridView");
                GridViewRow selectedRow = gridView.SelectedRow;
                string ID = selectedRow.Cells[0].Text;
                Session["ID"] = ID;
                Server.Transfer("~/EditUserPage.aspx");
            }
            else
            {
                MessageBox.Show("Error");
            }
                       
        }
        protected void DeteleUser(object sender, EventArgs e)
        {
            GridView gridView = (GridView)this.Page.FindControl("GridView");
            GridViewRow selectedRow = gridView.SelectedRow;
            string email = selectedRow.Cells[3].Text;
            string ID = selectedRow.Cells[0].Text;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
            connection.Open();
            string insert = "Delete from Users where ID = @ID and Email = @email";
            SqlCommand command = new SqlCommand(insert, connection);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("ID", ID);           
            command.ExecuteScalar();
            try
            {

                Response.Redirect("AdminPage.aspx");
                connection.Close();

            }
            catch (NullReferenceException)
            {

                MessageBox.Show("Error");
            }

            connection.Close();
        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {

        }
    }
}
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
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GoToAddUserPage(object sender, EventArgs e)
        {
            Response.Redirect("AddUserPage.aspx");
        }

        protected void EditUser(object sender, EventArgs e)
        {
            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
            //Console.WriteLine("test");
            //SqlCommand command = new SqlCommand("SELECT ID FROM Users WHERE password = @password and email  = @email", connection);
            //command.Parameters.AddWithValue("password", password);
            //command.Parameters.AddWithValue("email", email);
            //connection.Open();
            //try
            //{
            //    int id = (int)command.ExecuteScalar();
            //    if (email.Equals("admin"))
            //    {
            //        Response.Redirect("AdminPage.aspx");
            //    }

            //    else
            //    {
            //        Response.Redirect("UserPage.aspx");
            //    }
            //}
            //catch (NullReferenceException)
            //{

            //    MessageBox.Show("Błędny email lub hasło");
            //}

            //connection.Close();
        }
        protected void DeteleUser(object sender, EventArgs e)
        {
            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
            //Console.WriteLine("test");
            //SqlCommand command = new SqlCommand("SELECT ID FROM Users WHERE password = @password and email  = @email", connection);
            //command.Parameters.AddWithValue("password", password);
            //command.Parameters.AddWithValue("email", email);
            //connection.Open();
            //try
            //{
            //    int id = (int)command.ExecuteScalar();
            //    if (email.Equals("admin"))
            //    {
            //        Response.Redirect("AdminPage.aspx");
            //    }

            //    else
            //    {
            //        Response.Redirect("UserPage.aspx");
            //    }
            //}
            //catch (NullReferenceException)
            //{

            //    MessageBox.Show("Błędny email lub hasło");
            //}

            //connection.Close();
        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {

        }
    }
}
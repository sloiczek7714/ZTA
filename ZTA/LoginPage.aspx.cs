using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;

namespace ZTA
{
    public partial class LoginPage : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showPassword(object sender, EventArgs e)
        {

        }

        protected void Login(object sender, EventArgs e)
        {
            string email = emailTextBox.Text; ;
            string password = passwordTextBox.Text;
            if (email.Equals(""))
            {
                string msg ="Prosze wprowadzić adres e-mail!";
                Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));

            }
            else if (password.Equals(""))
            {
                string msg = "Prosze wprowadzić hasło!";
                Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));
            }
            else
            {
                
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                    SqlCommand command = new SqlCommand("SELECT User_ID FROM Users WHERE Password = @password and Email  = @email", connection);
                    password = Helper.HashPassword(password, email);
                    command.Parameters.AddWithValue("password", password);
                    command.Parameters.AddWithValue("email", email);
                    connection.Open();
                try
                {
                    int id = (int)command.ExecuteScalar();
                    Session["ID"] = id;
                    Response.Redirect("UserPage.aspx");
                    connection.Close();

                }
                catch (NullReferenceException)
                {
                    
                    string msg = "błędny email lub hasło";
                    Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));
                }

                
            }
        }
    }
}
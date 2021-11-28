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
                MessageBox.Show("Prosze wprowadzić adres e-mail!");

            }
            else if (password.Equals(""))
            {
                MessageBox.Show("Prosze wprowadzić adres hasło!");
            }
            else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
                Console.WriteLine("test");
                SqlCommand command = new SqlCommand("SELECT ID FROM Users WHERE Password = @password and Email  = @email", connection);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("email", email);
                connection.Open();
                try
                {
                    int id = (int)command.ExecuteScalar();
                    Session["ID"] = id;
                    if (email.Equals("admin"))
                    {
                        Response.Redirect("AdminPage.aspx");
                        Session.RemoveAll();
                    }

                    else 
                    {
                        
                        Response.Redirect("UserPage.aspx");
                        Session.RemoveAll();
                    }
                }
                catch (NullReferenceException)
                {

                    MessageBox.Show("Błędny email lub hasło");
                }

                connection.Close();
            }
        }
    }
}
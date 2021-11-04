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

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
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
                SqlCommand command = new SqlCommand("SELECT ID FROM Users WHERE password = @password and email  = @email", connection);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("email", email);
                connection.Open();
                try
                {
                    int id = (int)command.ExecuteScalar();
                    if (email.Equals("admin"))
                    {
                        Response.Redirect("AdminPage.aspx");
                    }

                    else
                    {
                        Response.Redirect("UserPage.aspx");
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
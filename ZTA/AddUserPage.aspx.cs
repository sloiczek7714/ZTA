using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class AddUserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
            protected void saveUser(object sender, EventArgs e)
        {

        }
        protected void createUser(object sender, EventArgs e)
        {
        //    string email = addEmailTextBox.Text; ;
        //    string password = passwordTextBox.Text;

        //    if (email.Equals(""))
        //    {
        //        MessageBox.Show("Prosze wprowadzić adres e-mail!");

        //    }
        //    else if (password.Equals(""))
        //    {
        //        MessageBox.Show("Prosze wprowadzić adres hasło!");
        //    }
        //    else
        //    {
        //        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
        //        Console.WriteLine("test");
        //        SqlCommand command = new SqlCommand("SELECT ID FROM Users WHERE password = @password and email  = @email", connection);
        //        command.Parameters.AddWithValue("password", password);
        //        command.Parameters.AddWithValue("email", email);
        //        connection.Open();
        //        try
        //        {
        //            int id = (int)command.ExecuteScalar();
        //            if (email.Equals("admin"))
        //            {
        //                Response.Redirect("AdminPage.aspx");
        //            }

        //            else
        //            {
        //                Response.Redirect("UserPage.aspx");
        //            }
        //        }
        //        catch (NullReferenceException)
        //        {

        //            MessageBox.Show("Błędny email lub hasło");
        //        }

        //        connection.Close();
        //    }
            Response.Redirect("AdminPage.aspx");
        }

    }
}
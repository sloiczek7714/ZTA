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
    public partial class AddUserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void saveUser(object sender, EventArgs e)
        {
            string email = addEmailTextBox.Text; ;
            string password = addPasswordTextBox.Text;
            string name = addNameTextBox.Text; ;
            string surname = addSurnameTextBox.Text; ;
            string position = addPositionTextBox.Text;
            string workPlace = addWorkPlaceTextBox.Text;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
            connection.Open();
            string insert = "Insert into Users (email, password, Name, Surname, Position, WorkPlace) values( @email, @password, @name, @surname,  @position, @workPlace)";
            SqlCommand command = new SqlCommand(insert, connection);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("position", position);
            command.Parameters.AddWithValue("workPlace", workPlace);
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

    }

}
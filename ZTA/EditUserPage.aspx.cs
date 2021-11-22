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
    public partial class EditUserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Session["ID"].ToString();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
            connection.Open();
            string insert = "Select Users set email=@email, Name=@name, Surname=@surname, Position=@position, WorkPlace=@workPlacewhere where ID = @ID";
            SqlCommand command = new SqlCommand(insert, connection);
            // editEmailTextBox.Text = selectedRow.Cells[0].Text;
            //  editNameTextBox.Text = selectedRow.Cells[1].Text;
            // editSurnameTextBox.Text = selectedRow.Cells[2].Text;
            //  editPositionTextBox.Text = selectedRow.Cells[3].Text;
            //  editWorkPlaceTextBox.Text = selectedRow.Cells[4].Text;
        }

        protected void SaveUser(object sender, EventArgs e)
        {
            string email = editEmailTextBox.Text;
            string name = editNameTextBox.Text; ;
            string surname = editSurnameTextBox.Text; ;
            string position = editPositionTextBox.Text;
            string workPlace = editWorkPlaceTextBox.Text;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
            connection.Open();
            string insert = "Update Users set email=@email, Name=@name, Surname=@surname, Position=@position, WorkPlace=@workPlacewhere where ID = @ID";
            SqlCommand command = new SqlCommand(insert, connection);
           // command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("ID", ID);
            command.Parameters.AddWithValue("name", name);
          //  command.Parameters.AddWithValue("surname", surname);
           // command.Parameters.AddWithValue("position", position);
           // command.Parameters.AddWithValue("workPlace", workPlace);





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
        protected void GoToUserPage(object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }
    }
}
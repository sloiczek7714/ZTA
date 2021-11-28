using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        string role;
        protected void selectRole(object sender, EventArgs e)
        {
            role = RoleList.SelectedValue.ToString();
        }
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
                    editEmailTextBox.Text = DataReader.GetValue(1).ToString();
                    editNameTextBox.Text = DataReader.GetValue(2).ToString();
                    editSurnameTextBox.Text = DataReader.GetValue(4).ToString();
                    editPositionTextBox.Text = DataReader.GetValue(5).ToString();
                    editWorkPlaceTextBox.Text = DataReader.GetValue(6).ToString();
                    //RoleList.SelectedValue = DataReader.GetValue(8).ToString();
                    editSystemNameTextBox.Text = DataReader.GetValue(7).ToString();
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
        protected void SaveUser(object sender, EventArgs e)
        {
            string ID = Session["ID"].ToString();
            string email = editEmailTextBox.Text;
            string name = editNameTextBox.Text; ;
            string surname = editSurnameTextBox.Text; ;
            string position = editPositionTextBox.Text;
            string workPlace = editWorkPlaceTextBox.Text;
            string systemName = editSystemNameTextBox.Text;
            role = RoleList.SelectedValue.ToString();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTAConnectionString"].ConnectionString);
            connection.Open();
            string update = "Update Users SET Email=@email, Name=@name, Surname=@surname, Position=@position,  WorkPlace=@workPlace, Role=@role, SystemName=@systemName where ID=@ID";
            SqlCommand command = new SqlCommand(update, connection);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("ID", ID);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("position", position);
            command.Parameters.AddWithValue("workPlace", workPlace);
            command.Parameters.AddWithValue("role", role);
            command.Parameters.AddWithValue("systemName", systemName);
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
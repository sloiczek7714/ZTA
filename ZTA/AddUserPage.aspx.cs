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
        string role = "Pracownik";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["ID"] != null)
            {
                string ID = Session["ID"].ToString();
                if (Helper.DoesUserHasPermission(ID, "Administrator"))
                {

                }

                else Response.Redirect("ErrorPage.aspx");
            }

            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void selectRole(object sender, EventArgs e)
        {
            role = RoleList.SelectedValue.ToString();
        }
        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }
        protected void saveUser(object sender, EventArgs e)
        {
            string email = addEmailTextBox.Text; ;
            string password = addPasswordTextBox.Text;
            string name = addNameTextBox.Text; ;
            string surname = addSurnameTextBox.Text; ;
            string position = addPositionTextBox.Text;
            string workPlace = addWorkPlaceTextBox.Text;
            string bossID = addBossIDTextBox.Text;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            string insert = "Insert into Users (Email, Password, Name, Surname, Position, WorkPlace, Role) values( @email, @password, @name, @surname,  @position, @workPlace, @role)";
            string select = "Select User_ID From Users WHERE Email=@email AND Role=@role";
            SqlCommand cmd = new SqlCommand(select,connection);
            SqlCommand command = new SqlCommand(insert, connection);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("position", position);
            command.Parameters.AddWithValue("workPlace", workPlace);
            command.Parameters.AddWithValue("role", role);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("role", role);
            command.ExecuteScalar();
            int user_ID = (int) cmd.ExecuteScalar();
            if (!String.IsNullOrEmpty(bossID))
                {
                string insertBoss = "Insert into Users_Boss (User_ID, Boss_ID) values(@ID, @bossID)";
                SqlCommand commandBoss = new SqlCommand(insertBoss, connection);
                commandBoss.Parameters.AddWithValue("ID", user_ID);
                commandBoss.Parameters.AddWithValue("bossID", bossID);
                commandBoss.ExecuteScalar();
            }
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
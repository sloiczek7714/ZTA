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
    public partial class AddUserPage : System.Web.UI.Page
    {
        public string role;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ID"] != null)
            {
                string ID = Session["ID"].ToString();
                if (Helper.DoesUserHasPermission(ID, "Administrator"))
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                    connection.Open();
                    SqlCommand com = new SqlCommand("SELECT * from Users  WHERE Role='Kierownik'", connection);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    DropDownBossList.DataTextField = ds.Tables[0].Columns["Email"].ToString();
                    DropDownBossList.DataSource = ds.Tables[0];
                    DropDownBossList.DataBind();
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
            if (role != "Pracownik")
            {
                DropDownBossList.Visible = false;
                BossLabel.Visible = false;
            }
            else
            {
                DropDownBossList.Visible = true;
                BossLabel.Visible = true;
            }

        }
        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }
        protected void saveUser(object sender, EventArgs e)
        {
            string email = addEmailTextBox.Text;
            string name = addNameTextBox.Text;
            string password = Helper.HashPassword(addPasswordTextBox.Text, email);
            string surname = addSurnameTextBox.Text;
            string position = addPositionTextBox.Text;
            string workPlace = addWorkPlaceTextBox.Text;
            string bossEmail = DropDownBossList.Text;
            role = RoleList.Text;
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(surname) || String.IsNullOrEmpty(position) || String.IsNullOrEmpty(workPlace))
            {
                string msg = "Uzupełnij wszystkie pola!";
                Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));
            }
            else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                connection.Open();
                string insert = "Insert into Users (Email, Password, Name, Surname, Position, WorkPlace, Role) values( @email, @password, @name, @surname,  @position, @workPlace, @role)";
                string select = "Select User_ID From Users WHERE Email=@email AND Role=@role";
                string selectBossID = "Select User_ID From Users WHERE Email=@bossEmail";
                SqlCommand cmd = new SqlCommand(select, connection);
                SqlCommand command = new SqlCommand(insert, connection);
                SqlCommand commandSelectBoss = new SqlCommand(selectBossID, connection);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("surname", surname);
                command.Parameters.AddWithValue("position", position);
                command.Parameters.AddWithValue("workPlace", workPlace);
                command.Parameters.AddWithValue("role", role);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("role", role);
                commandSelectBoss.Parameters.AddWithValue("bossEmail", bossEmail);
                command.ExecuteScalar();
                int user_ID = (int)cmd.ExecuteScalar();
                if (!String.IsNullOrEmpty(bossEmail) && role.Equals("Pracownik"))
                {
                    int bossID = (int)commandSelectBoss.ExecuteScalar();
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
                    string msg = "Error";
                    Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));
                }
                connection.Close();
            }
        }
    }
}
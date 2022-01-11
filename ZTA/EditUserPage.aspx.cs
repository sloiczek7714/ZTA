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
        public string role;
        protected void selectRole(object sender, EventArgs e)
        {
            role = RoleList.SelectedValue.ToString();
            if (role != "Pracownik")
            {
                EditDropDownBossList.Visible = false;
                editBossLabel.Visible = false;
            }
            else
            {
                EditDropDownBossList.Visible = true;
                editBossLabel.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                string UserID = Session["ID"].ToString();
                if (!Page.IsPostBack)
                {
                    string ID = Session["IDEditUser"].ToString();

                    if (Helper.DoesUserHasPermission(UserID, "Administrator"))
                    {
                        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                        connection.Open();
                        string insert = "SELECT Users.User_ID,  Users.Name, Users.Surname, Users.Position, Users.WorkPlace, Users.Email, Users.Role, Users_Boss.Boss_ID, Users.Password FROM Users  LEFT JOIN Users_Boss ON Users.User_ID = Users_Boss.User_ID where Users.User_ID = @ID";
                        SqlCommand command = new SqlCommand(insert, connection);
                        command.Parameters.AddWithValue("ID", ID);
                        SqlDataReader DataReader = command.ExecuteReader();
                        if (DataReader.Read())
                        {
                            editNameTextBox.Text = DataReader.GetValue(1).ToString();
                            editSurnameTextBox.Text = DataReader.GetValue(2).ToString();
                            editPositionTextBox.Text = DataReader.GetValue(3).ToString();
                            editWorkPlaceTextBox.Text = DataReader.GetValue(4).ToString();
                            editEmailTextBox.Text = DataReader.GetValue(5).ToString();
                            RoleList.SelectedValue = DataReader.GetValue(6).ToString();
                            Session["tempBoss"] = DataReader.GetValue(7).ToString();
                            DataReader.Close();
                        }                        
                        SqlCommand com = new SqlCommand("SELECT * from Users  WHERE Role='Kierownik' and Users.Email!=@email", connection);
                        com.Parameters.AddWithValue("email",editEmailTextBox.Text);
                        SqlDataAdapter da = new SqlDataAdapter(com);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        EditDropDownBossList.DataTextField = ds.Tables[0].Columns["Email"].ToString();
                        EditDropDownBossList.DataSource = ds.Tables[0];
                        EditDropDownBossList.DataBind();
                        if (!Helper.DoesUserHasPermission(ID, "Pracownik"))
                        {
                            EditDropDownBossList.Visible = false;
                            editBossLabel.Visible = false;
                        }
                    }
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
            string ID = Session["IDEditUser"].ToString();
            string email = editEmailTextBox.Text;
            string name = editNameTextBox.Text; ;
            string surname = editSurnameTextBox.Text; ;
            string position = editPositionTextBox.Text;
            string workPlace = editWorkPlaceTextBox.Text;
            string bossEmail = EditDropDownBossList.Text;
            string password = Helper.HashPassword(editPasswordTextBox.Text, email);
            string updateBoss = "";
            role = RoleList.Text;

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(surname) || String.IsNullOrEmpty(position) || String.IsNullOrEmpty(workPlace))
            {
                string msg = "Uzupełnij wszystkie pola!";
                Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));
            }
            else
            {
                try
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                    connection.Open();
                    string update = "Update Users SET Users.Email=@email, Users.Name=@name, Users.Surname=@surname, Users.Position=@position,  Users.WorkPlace=@workPlace, Users.Role=@role From Users LEFT join Users_Boss ON Users.User_ID = Users_Boss.User_ID where Users.User_ID=@ID";
                    
                    if (!String.IsNullOrEmpty(editPasswordTextBox.Text))
                    {
                        update = "Update Users SET Users.Email=@email, Users.Name=@name, Users.Password=@password, Users.Surname=@surname, Users.Position=@position,  Users.WorkPlace=@workPlace, Users.Role=@role From Users LEFT join Users_Boss ON Users.User_ID = Users_Boss.User_ID where Users.User_ID=@ID";
                    }
                    SqlCommand command = new SqlCommand(update, connection);
                    if (String.IsNullOrEmpty(Session["tempBoss"].ToString()) && role != "Kierownik" && role !="Administrator")
                    {
                        updateBoss = "Insert into Users_Boss (User_ID, Boss_ID) values(@ID, @bossID)";
                        string selectBossID = "Select User_ID From Users WHERE Email=@bossEmail";
                        SqlCommand commandSelectBoss = new SqlCommand(selectBossID, connection);
                        commandSelectBoss.Parameters.AddWithValue("bossEmail", bossEmail);
                        int user_ID = (int)commandSelectBoss.ExecuteScalar();
                        SqlCommand commandBoss = new SqlCommand(updateBoss, connection);
                        commandBoss.Parameters.AddWithValue("bossID", user_ID);
                        commandBoss.Parameters.AddWithValue("ID", ID);
                        commandBoss.ExecuteScalar();

                    }
                    else if (String.IsNullOrEmpty(bossEmail) || role != "Pracownik")
                    {
                        updateBoss = "Delete Users_Boss WHERE User_ID = @ID";
                        SqlCommand commandBoss = new SqlCommand(updateBoss, connection);
                        commandBoss.Parameters.AddWithValue("ID", ID);
                        commandBoss.ExecuteScalar();
                    }
                    else if (!String.IsNullOrEmpty(bossEmail) || role =="Pracownik")
                    {
                        updateBoss = "Update Users_Boss Set Users_Boss.Boss_ID = @bossID WHERE Users_Boss.User_ID=@ID ";
                        string selectBossID = "Select User_ID From Users WHERE Email=@bossEmail";
                        SqlCommand commandSelectBoss = new SqlCommand(selectBossID, connection);
                        commandSelectBoss.Parameters.AddWithValue("bossEmail", bossEmail);
                        int user_ID = (int)commandSelectBoss.ExecuteScalar();
                        SqlCommand commandBoss = new SqlCommand(updateBoss, connection);
                        commandBoss.Parameters.AddWithValue("bossID", user_ID);
                        commandBoss.Parameters.AddWithValue("ID", ID);
                        commandBoss.ExecuteScalar();
                    }
                    command.Parameters.AddWithValue("email", email);
                    command.Parameters.AddWithValue("ID", ID);
                    command.Parameters.AddWithValue("password", password);

                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("surname", surname);
                    command.Parameters.AddWithValue("position", position);
                    command.Parameters.AddWithValue("workPlace", workPlace);
                    command.Parameters.AddWithValue("role", role);
                    command.ExecuteScalar();
                    Response.Redirect("AdminPage.aspx");
                    connection.Close();

                }
                catch (NullReferenceException)
                {

                    string msg = "Error";
                    Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));
                }

            }
        }
        protected void GoToUserPage(object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }
    }
}
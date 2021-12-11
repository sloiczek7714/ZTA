using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class ListOfProcedures : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)

            {
                if (Session["ID"] != null)
                {
                    string ID = Session["ID"].ToString();
                    ZTA.SelectParameters.Add("ID", ID);
                    if (Helper.DoesUserHasPermission(ID, "Administrator"))
                    {
                        try
                        {
                           
                            ZTA.SelectCommand = "SELECT Form.Form_ID,Form.System_Name,Form.Begin_Date ,Form.End_Date,Form.Comment,Form.User_ID ,Users.Name as 'Name' ,Users.Surname as 'Surname', Users_Boss.Boss_ID as 'Kierownik' FROM [dbo].[Form] left join Users on Users.User_ID = Form.User_ID left join Users_Boss on Users_Boss.User_ID = Users.User_ID ";
                           
                        }
                        catch
                        {
                            TextBox textBox = new TextBox();
                            textBox.Text = "Nie masz zadnych proecedur";
                        }
                    }

                    else if (Helper.DoesUserHasPermission(ID, "Kierownik"))
                    {
                        try
                        {
                            
                            ZTA.SelectCommand = "SELECT Form.Form_ID,Form.System_Name,Form.Begin_Date ,Form.End_Date,Form.Comment,Form.User_ID ,Users.Name as 'Name',Users.Surname as 'Surname', Users_Boss.Boss_ID as 'Kierownik' FROM [dbo].[Form] left join Users on Users.User_ID = Form.User_ID left join Users_Boss on Users_Boss.User_ID = Users.User_ID where Users_Boss.Boss_ID =@ID ";
                        }
                        catch
                        {
                            TextBox textBox = new TextBox();
                            textBox.Text = "Nie masz zadnych proecedur";
                        }
                    }

                    else if (Helper.DoesUserHasPermission(ID, "Pracownik"))
                    {
                        try
                        {
                            ZTA.SelectCommand = "SELECT Form.Form_ID,Form.System_Name,Form.Begin_Date ,Form.End_Date,Form.Comment,Form.User_ID ,Users.Name as 'Name' ,Users.Surname as 'Surname' , Users_Boss.Boss_ID as 'Kierownik' FROM [dbo].[Form] left join Users on Users.User_ID = Form.User_ID  left join Users_Boss on Users_Boss.User_ID = Users.User_ID where Users.Users_ID =@ID";
                        }
                        catch
                        {
                            
                            TextBox textBox = new TextBox();
                            textBox.Text = "Nie masz zadnych proecedur";
                        }
                    }
                }

                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }

        protected void goToNewProcedure(object sender, EventArgs e)
        {
            Session["ID"] = 24;
            Response.Redirect("NewProcedurePage.aspx");
        }

        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }
        protected void ZTA_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
       
    }
}
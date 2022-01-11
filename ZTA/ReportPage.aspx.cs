using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;
using System.Data.SqlClient;
using System.Configuration;

namespace ZTA
{
    public partial class ReportPage : System.Web.UI.Page
    {
        public int a;
        public int b;
        string bossID;
        public string r;
        public int restActivities { get { return a; } set { } }
        public int endedActivities { get { return b; } set { } }
        public string raport { get { return r; } set { } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                string ID = Session["ID"].ToString();
                string formID = Session["formID"].ToString();
                a = 5;
                b = 21;
                
                ZTA.SelectParameters.Add("formID", formID);
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                try
                {
                    ZTA.SelectCommand = "SELECT Activity.Activity_ID as 'Numer_czynnosci', Activity.Activity as 'Czynnosc', Answer.Comment as 'Komentarz', Format(Answer.Answer_Date ,'MM/dd/yyyy hh:mm') as 'AData' FROM Activity left join Answer on Answer.Activity_ID = Activity.Activity_ID left join Form on Answer.Form_ID = Form.Form_ID left join Users on Users.User_ID = Form.User_ID WHERE Form.Form_ID = @formID";
                }

                catch
                {
                    Response.Redirect("ErrorPage.aspx");
                }
                try
                {                   
                    connection.Open();
                    GridView gridView = (GridView)this.Page.FindControl("reportGridView");
                    foreach (GridViewRow row in gridView.Rows)
                    {
                        row.Cells[1].Text = row.Cells[1].Text.Replace("\n", "<br/>");
                    }
                    SqlCommand select = new SqlCommand("SELECT Form.System_Name,FORMAT(Form.Begin_Date ,'MM/dd/yyyy hh:mm')as 'Begin_Date' ,FORMAT(Form.End_Date ,'MM/dd/yyyy hh:mm') as 'End_Date', Form.Comment, Users.Name as 'Name', Users.Surname as 'Surname', Form.User_ID, Users_Boss.Boss_ID FROM Form join Users on Users.User_ID = Form.User_ID left join Users_Boss on Users_Boss.User_ID=Users.User_ID WHERE Form.Form_ID = @formID", connection);
                    select.Parameters.AddWithValue("formID", formID);
                    var reader = select.ExecuteReader();
                    while (reader.Read())
                    {
                        systemNamelabel.Text = "Nazwa systemu: " + reader[0].ToString();
                        r = "raport"+ systemNamelabel.Text.TrimEnd(' ') + ".pdf";
                        beginDatelabel.Text = "Data rozpoczęcia: " + reader[1].ToString();
                        endDatelabel.Text = "Data zakończenia: " + reader[2].ToString();
                        employelabel.Text = "Imię i nazwisko pracownika: " + reader[4].ToString() + " " + reader[5].ToString() + " ID: " + reader[6].ToString();
                        bossID = reader[7].ToString();
                        commentlabel.Text = "Komentarz: " + reader[3].ToString();
                    }
                    reader.Close();
                    if (!String.IsNullOrEmpty(bossID))
                    {
                        SqlCommand selectBoss = new SqlCommand("Select Users.Name, Users.Surname From Users WHERE Users.User_ID=@bossID", connection);
                        selectBoss.Parameters.AddWithValue("bossID", bossID);
                        var DataReader = selectBoss.ExecuteReader();
                        while (DataReader.Read())
                        {
                            bosslabel.Text = "Imię i nazwisko kierownika: " + DataReader[0].ToString()+ " " + DataReader[1].ToString() + " ID: " + bossID;
                            DataReader.Close();
                        }
                    }                   

                }
                catch
                {
                    TextBox textBox = new TextBox();
                    textBox.Text = "Nie masz zadnych proecedur";
                }

                try
                {
                    SqlCommand select = new SqlCommand(" SELECT sum(case when Answer_Date is null then 0 else 1 end) FROM[dbo].[Answer]  where Answer.Form_ID=@formID", connection);
                    select.Parameters.AddWithValue("formID", formID);
                    string i = select.ExecuteScalar().ToString();
                    b = Int32.Parse(i);
                    a = 21 - b;

                }
                catch
                {
                    Response.Redirect("ErrorPage.aspx");
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

    }
}

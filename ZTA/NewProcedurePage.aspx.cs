using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class NewProcedure : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["beginDate"]= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (Session["ID"] != null)
            {
                string ID = Session["ID"].ToString();
                
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
        protected void saveProcedure(object sender, EventArgs e)
        {
            
            ID = Session["ID"].ToString();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand insertForm = new SqlCommand("Insert into Form(System_Name, Begin_Date, Comment, User_ID) values (@systemName, @begin_date, @comment, @user_ID);" + "SELECT SCOPE_IDENTITY()", connection);
            insertForm.Parameters.AddWithValue("systemName",SystemName.Text);
            insertForm.Parameters.AddWithValue("user_ID",ID);
            insertForm.Parameters.AddWithValue("begin_date",Session["beginDate"].ToString());
            insertForm.Parameters.AddWithValue("comment", overallComment.Text);
             var form_ID = insertForm.ExecuteScalar();
            GridView gridView = (GridView)this.Page.FindControl("procedureGridView");
            foreach (GridViewRow row in gridView.Rows)
            {
                SqlCommand insertAnswer = new SqlCommand("Insert into Answer(Answer_Date, Comment, Form_ID, Activity_ID) values (@answerDate, @comment, @form_ID, @activity_ID)", connection);
                TextBox dateTextBox = (TextBox)row.FindControl("dateTextBox");
                TextBox commentTextBox = (TextBox)row.FindControl("commentTextBox");                
                string comment = commentTextBox.Text;
                string date = dateTextBox.Text;
                if (String.IsNullOrEmpty(date))
                {
                    insertAnswer = new SqlCommand("Insert into Answer(Comment, Form_ID, Activity_ID) values (@comment, @form_ID, @activity_ID)", connection);
                }
                
                var activity_ID = row.Cells[0].Text;
                System.Diagnostics.Debug.WriteLine(comment); 
                System.Diagnostics.Debug.WriteLine(date); 
                System.Diagnostics.Debug.WriteLine(activity_ID); 
                insertAnswer.Parameters.AddWithValue("comment", comment);
                insertAnswer.Parameters.AddWithValue("answerDate", date);
                insertAnswer.Parameters.AddWithValue("form_ID", form_ID);
                insertAnswer.Parameters.AddWithValue("activity_ID", activity_ID);
                insertAnswer.ExecuteScalar();
            }
           
        }

        protected void endProcedure(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("Insert into Form(System_Name, Begin_Date, End_Date, User_ID) values (@systemName, @begin_date, @user_ID)", connection);
            command.Parameters.AddWithValue("systemName", SystemName.Text);
            command.Parameters.AddWithValue("user_ID", ID);
            //  command.Parameters.AddWithValue("begin_date",beginDate);
            command.ExecuteScalar();
        }
            protected void ZTA_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
       
    }
}
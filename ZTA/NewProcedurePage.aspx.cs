﻿using System;
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
    public partial class NewProcedure : System.Web.UI.Page
    {
        new string ID;
        string form_ID;
        string flag;
        protected void Page_Load(object sender, EventArgs e)
        { 
            Session["beginDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (Session["ID"] != null)
            {
                ID = Session["ID"].ToString();
                GridView gridView = (GridView)this.Page.FindControl("procedureGridView");
                foreach (GridViewRow row in gridView.Rows)
                {
                    row.Cells[1].Text = row.Cells[1].Text.Replace("\n", "<br/>");
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
        protected void saveProcedure(object sender, EventArgs e)
        {
            save();
        }
        
        protected void save()
        { 
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand check = new SqlCommand("SELECT FORM_ID FROM Form WHERE System_Name=@systemName AND User_ID=@User_ID",connection);
            check.Parameters.AddWithValue("User_ID", ID);
            check.Parameters.AddWithValue("systemName", SystemName.Text);
            try
            {
                flag = check.ExecuteScalar().ToString();
            }
            catch
            {
               flag = "";
            }
            if (String.IsNullOrEmpty(flag))
            {
                if ( String.IsNullOrEmpty(SystemName.Text))
                {
                    string msg = "Wpisz nazwę systemu!";
                    Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + msg.Replace("'", "\\'") + "') </script>"));
                }
                    else {
                            SqlCommand insertForm = new SqlCommand("Insert into Form(System_Name, Begin_Date, Comment, User_ID) values (@systemName, @begin_date, @comment, @user_ID);" + "SELECT SCOPE_IDENTITY()", connection);
                            insertForm.Parameters.AddWithValue("systemName", SystemName.Text);
                            insertForm.Parameters.AddWithValue("user_ID", ID);
                            insertForm.Parameters.AddWithValue("begin_date", Session["beginDate"].ToString());
                            insertForm.Parameters.AddWithValue("comment", overallComment.Text);
                            string form_ID = insertForm.ExecuteScalar().ToString();
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
                                insertAnswer.Parameters.AddWithValue("comment", comment);
                                insertAnswer.Parameters.AddWithValue("answerDate", date);
                                insertAnswer.Parameters.AddWithValue("form_ID", form_ID);
                                insertAnswer.Parameters.AddWithValue("activity_ID", activity_ID);
                                insertAnswer.ExecuteScalar();
                            }
                            Session["formID"] = form_ID;
                        }
             }
            else 
            {
                SqlCommand insertForm = new SqlCommand("Update Form SET Comment=@comment where From_ID=@formID)", connection);
                insertForm.Parameters.AddWithValue("formID", flag);
                insertForm.Parameters.AddWithValue("comment", overallComment.Text);
                GridView gridView = (GridView)this.Page.FindControl("procedureGridView");
                foreach (GridViewRow row in gridView.Rows)
                {
                    SqlCommand updateAnswer = new SqlCommand("Update Answer SET Answer_Date=@answerDate, Comment=@comment WHERE Form_ID=@formID AND Activity_ID=@activity_ID", connection);
                    TextBox dateTextBox = (TextBox)row.FindControl("dateTextBox");
                    TextBox commentTextBox = (TextBox)row.FindControl("commentTextBox");
                    string comment = commentTextBox.Text;
                    string date = dateTextBox.Text;
                    if (String.IsNullOrEmpty(date))
                    {
                        updateAnswer = new SqlCommand("Update Answer SET Comment=@comment WHERE Form_ID = @formID AND Activity_ID=@activity_ID", connection);
                    }
                    var activity_ID = row.Cells[0].Text;
                    updateAnswer.Parameters.AddWithValue("comment", comment);
                    updateAnswer.Parameters.AddWithValue("answerDate", date);
                    updateAnswer.Parameters.AddWithValue("formID", flag);
                    updateAnswer.Parameters.AddWithValue("activity_ID", activity_ID);
                    updateAnswer.ExecuteScalar();
                }
            }
        }
        
        protected void endProcedure(object sender, EventArgs e)
        {
            save();
            form_ID = Session["formID"].ToString();
            string end_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("Update Form SET End_Date=@end_date WHERE Form_ID=@formID", connection);
            command.Parameters.AddWithValue("formID", form_ID);
            command.Parameters.AddWithValue("end_date", end_Date);
            command.ExecuteScalar();
            Response.Redirect("ListOfProceduresPage.aspx");
        }
        protected void ZTA_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            
        }

        protected void showHint(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = procedureGridView.Rows[index];

                if (index == 12)
                {
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('TablePage.aspx');", true);
                }
                else
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select Tips from Activity WHERE Activity_ID=@index", connection);
                    command.Parameters.AddWithValue("index", (1 + index));
                    string tip = (string)command.ExecuteScalar();
                    string title = "Pomoc";
                    Page.Controls.Add(new LiteralControl("<script language='javascript'>window.alert('" + tip.Replace("'", "\\'") + "') </script>"));

                }
            }
        }
       
    }
}
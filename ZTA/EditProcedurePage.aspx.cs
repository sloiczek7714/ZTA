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
    public partial class EditProcedure : System.Web.UI.Page
    {
        string formID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                ID = Session["ID"].ToString();
                if (Session["formID"].ToString() != null)
                {
                    string UserID = Session["ID"].ToString();
                    formID = Session["formID"].ToString();
                    if (!Page.IsPostBack)
                    {
                        formID = Session["formID"].ToString();
                        if (Session["endDate"].ToString() == "0" || Helper.DoesUserHasPermission(UserID, "Administrator"))
                        {
                            ZTA.SelectParameters.Add("formID", formID);
                            ZTA.SelectCommand = "SELECT Activity.Activity_ID as 'Numer_czynnosci', Activity.Activity as 'Czynnosc', Answer.Comment as 'Komentarz', FORMAT(Answer.Answer_Date,'MM/dd/yyyy hh:mm') as 'Data' FROM Activity left join Answer on Answer.Activity_ID = Activity.Activity_ID left join Form on Answer.Form_ID = Form.Form_ID WHERE Form.Form_ID = @formID";
                            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
                            connection.Open();
                            string select = "SELECT System_Name, Comment FROM Form WHERE Form_Id=@formID";
                            SqlCommand command = new SqlCommand(select, connection);
                            command.Parameters.AddWithValue("formID", formID);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                editOverallComment.Text = reader[1].ToString();
                                SystemNameLabel.Text = reader[0].ToString();
                            }
                            reader.Close();
                            int i = 1;
                            foreach (GridViewRow row in procedureGridView.Rows)
                            {
                                string selectDateComment = "SELECT Answer.Comment, FORMAT(Answer.Answer_Date,'MM/dd/yyyy hh:mm') FROM Answer left join Activity on Answer.Activity_ID = Activity.Activity_ID left join Form on Answer.Form_ID = Form.Form_ID WHERE Form.Form_ID = @formID AND Activity.Activity_ID=@ActivityID";
                                SqlCommand comm = new SqlCommand(selectDateComment, connection);
                                comm.Parameters.AddWithValue("formID", formID);
                                comm.Parameters.AddWithValue("ActivityID", i);
                                TextBox dateTextBox = (TextBox)row.FindControl("dateTextBox");
                                TextBox commentTextBox = (TextBox)row.FindControl("commentTextBox");
                                var Datareader = comm.ExecuteReader();
                                while (Datareader.Read())
                                {
                                    commentTextBox.Text = Datareader[0].ToString();
                                    dateTextBox.Text = Datareader[1].ToString();
                                }
                                Datareader.Close();
                                if (!String.IsNullOrEmpty(dateTextBox.Text))
                                {
                                    dateTextBox.Enabled = false;
                                }
                                i++;
                            }
                            GridView gridView = (GridView)this.Page.FindControl("procedureGridView");
                            foreach (GridViewRow row in gridView.Rows)
                            {
                                row.Cells[1].Text = row.Cells[1].Text.Replace("\n", "<br/>");
                            }

                            connection.Close();
                           
                        }

                        else
                        {
                            Response.Redirect("ErrorPage.aspx");
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

        protected void saveProcedure(object sender, EventArgs e)
        {
            save();
        }
        protected void save()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand insertForm = new SqlCommand("Update Form SET Comment=@comment where From_ID=@formID)", connection);
            insertForm.Parameters.AddWithValue("formID", formID);
            insertForm.Parameters.AddWithValue("comment", editOverallComment.Text);
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
                updateAnswer.Parameters.AddWithValue("formID", formID);
                updateAnswer.Parameters.AddWithValue("activity_ID", activity_ID);
                updateAnswer.ExecuteScalar();
            }
        }

        protected void endProcedure(object sender, EventArgs e)
        {
            save();
            string end_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ZTADBConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("Update Form SET End_Date=@end_date WHERE Form_ID=@formID", connection);
            command.Parameters.AddWithValue("formID", formID);
            command.Parameters.AddWithValue("end_date", end_Date);
            command.ExecuteScalar();
            Response.Redirect("ListOfProceduresPage.aspx");
        }
        protected void ZTA_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
        protected void procedureGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
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
                    MessageBox.Show(tip, title);

                }
            }
        }
    }
}
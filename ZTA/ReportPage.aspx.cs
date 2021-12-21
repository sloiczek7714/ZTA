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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Data.SqlClient;
using System.Configuration;

namespace ZTA
{
    public partial class ReportPage : System.Web.UI.Page
    {
        public int a;
        public int b;
        string bossID;
        public int restActivities { get { return a; } set { } }
        public int endedActivities { get { return b; } set { } }

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
                    SqlCommand select = new SqlCommand("SELECT Form.System_Name,FORMAT(Form.Begin_Date ,'MM/dd/yyyy hh:mm')as 'Begin_Date' ,FORMAT(Form.End_Date ,'MM/dd/yyyy hh:mm') as 'End_Date', Form.Comment, Users.Name as 'Name', Users.Surname as 'Surname', Form.User_ID, Users_Boss.Boss_ID FROM Form join Users on Users.User_ID = Form.User_ID left join Users_Boss on Users_Boss.User_ID=Users.User_ID WHERE Form.Form_ID = @formID", connection);
                    select.Parameters.AddWithValue("formID", formID);
                    var reader = select.ExecuteReader();
                    while (reader.Read())
                    {
                        systemNamelabel.Text = "Nazwa systemu: " + reader[0].ToString();
                        beginDatelabel.Text = "Data rozpoczęcia: " + reader[1].ToString();
                        endDatelabel.Text = "Data zakończenia: " + reader[2].ToString();
                        employelabel.Text = "Imię i nazwisko pracownika: " + reader[4].ToString() + reader[5].ToString() + "ID:" + reader[6].ToString();
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
                            bosslabel.Text = "Imię i nazwisko kierownika: " + DataReader[0].ToString()+ DataReader[1].ToString() + "ID: " + bossID;
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
            //var uri = new Uri("http://localhost:44341/ReportPage.aspx");
            //var urlToPdf = new ChromePdfRenderer();
            //var pdf = urlToPdf.RenderUrlAsPdf(uri);
            //pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "UrlToPdfExample1.Pdf"));

        }
        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        protected void pdrCreate(object sender, EventArgs e)
        {
            ExportGridToPDF();
            //try
            //{
            //    Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
            //    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //    pdfDoc.Open();
            //    Paragraph Text = new Paragraph("This is test file");
            //    pdfDoc.Add(Text);
            //    pdfWriter.CloseStream = false;
            //    pdfDoc.Close();
            //    Response.Buffer = true;
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-disposition", "attachment;filename=Example.pdf");
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.Write(pdfDoc);
            //    Response.End();
            //}
            //catch (Exception ex)
            //{ Response.Write(ex.Message); }
        }
        private void ExportGridToPDF()
        {

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    reportGridView.AllowPaging = false;
                    reportGridView.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }

        }

    }
}

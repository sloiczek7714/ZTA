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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                string ID = Session["ID"].ToString();
                string formID = Session["formID"].ToString();
                ZTA.SelectParameters.Add("formID", formID);
                //if (Helper.DoesUserHasPermission(ID, "Administrator"))
                //{
                try
                {
                    ZTA.SelectCommand = "SELECT Activity.Activity_ID as 'Numer_czynnosci', Activity.Activity as 'Czynnosc', Answer.Comment as 'Komentarz', CONVERT(Answer.Answer_Date ,'MM/dd/yyyy hh:mm')as 'Data' FROM Activity left join Answer on Answer.Activity_ID = Activity.Activity_ID left join Form on Answer.Form_ID = Form.Form_ID left join Users on Users.User_ID = Form.User_ID WHERE Form.Form_ID = @formID";
                }
                catch
                {
                    TextBox textBox = new TextBox();
                    textBox.Text = "Nie masz zadnych proecedur";
                }
                //}

                //else if (Helper.DoesUserHasPermission(ID, "Kierownik"))
                //{
                //    try
                //    {
                //        ZTA.SelectCommand = "SELECT Form.Form_ID,Form.System_Name,Form.Begin_Date ,Form.End_Date,Form.Comment,Form.User_ID ,Users.Name as 'Name',Users.Surname as 'Surname', Users_Boss.Boss_ID as 'Kierownik' FROM [dbo].[Form] left join Users on Users.User_ID = Form.User_ID left join Users_Boss on Users_Boss.User_ID = Users.User_ID where Users_Boss.Boss_ID =@ID ";
                //    }
                //    catch
                //    {
                //        TextBox textBox = new TextBox();
                //        textBox.Text = "Nie masz zadnych proecedur";
                //    }
                //}

                //else if (Helper.DoesUserHasPermission(ID, "Pracownik"))
                //{
                //    try
                //    {
                //        ZTA.SelectCommand = "SELECT Form.Form_ID,Form.System_Name,Form.Begin_Date ,Form.End_Date,Form.Comment,Form.User_ID ,Users.Name as 'Name' ,Users.Surname as 'Surname' , Users_Boss.Boss_ID as 'Kierownik' FROM [dbo].[Form] left join Users on Users.User_ID = Form.User_ID  left join Users_Boss on Users_Boss.User_ID = Users.User_ID where Users.User_ID =@ID";
                //    }
                //    catch
                //    {
                //        TextBox textBox = new TextBox();
                //        textBox.Text = "Nie masz zadnych proecedur";
                //    }
                //}
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
                    GridView1.AllowPaging = false;
                    GridView1.RenderControl(hw);
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

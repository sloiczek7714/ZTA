using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZTA
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void showExplanation1(object sender, System.EventArgs e)
        {
            // if (CheckBox2.Checked)
            // {
            Label1.Visible = true;
            Label1.Text = "lalallalala";
            Label1.Text="1.1.	Należy przeanalizować źródła danych dostępnych w przedsiębiorstwie oraz usługi,      z których ono korzysta, a następnie sporządzić listę, które z nich będą uznawane za zasoby. Następnym krokiem jest zatwierdzenie listy oraz udostępnienie jej zgodnie z polityką bezpieczeństwa w przedsiębiorstwie. ";
           // }
        
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                CheckBox1.Text = "Checked";
            }
            else
            {
                CheckBox1.Text = "Unchecked";
            }
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
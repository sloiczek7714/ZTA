<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="ZTA.WebForm2" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 394px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            Witam! Aby rozpoczać proszę się zalogować.</p>
        <a href="javascript:__doPostBack('LinkButton1','')">G</a>mail - do pomyślenia<br />
  
            <div style="height: 183px">
                <asp:Label ID="Label1" runat="server" Text="E-mail"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Hasło"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Zaloguj" />
                <br />
        </div>
          
    </form>
</body>
</html>

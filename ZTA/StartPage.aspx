<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="ZTA.StartPage" %>


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
            Witam! Aby rozpoczać proszę się zalogować.
        </p>
        <br />

        <div style="height: 183px">
            <asp:Label ID="emailLabel" runat="server" Text="E-mail"></asp:Label>
            <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="passwordLabel" runat="server" Text="Hasło"></asp:Label>
            <asp:TextBox ID="passwordTextBox" runat="server" OnTextChanged="TextBox2_TextChanged" type="password"></asp:TextBox>
            <br />
            <asp:Button ID="LoginButton" runat="server" OnClick="Login" Text="Zaloguj" />
            <br />
            <br />
        </div>

    </form>
</body>
</html>

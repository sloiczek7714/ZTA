<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="ZTA.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/SiteStyle.css" rel="sitestyle" type="text/css" />

</head>
<body>
    <form class="html" runat="server">
        <p>
            Witam! Aby rozpoczać proszę się zalogować.
        </p>
        <br />

        <div class="body"
            <asp:Label ID="emailLabel" runat="server" Text="E-mail"></asp:Label>
            <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
           <%-- <asp:RegularExpressionValidator ID="EmailValidator" runat="server"
                ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic"
                ErrorMessage="Nieprawidłowy adres" ControlToValidate="emailTextBox"
                ForeColor="Red"> </asp:RegularExpressionValidator>--%>
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

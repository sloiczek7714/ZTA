<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="ZTA.StartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
    footer {
    position: absolute;
    left: 0;
    bottom: 0;
    width: 100%;
    white-space: nowrap;
    line-height: 60px;
    text-align: center;
    margin-top: -200px;
    }
    html {
    position: relative;
    min-height: 100%;
}
body {
    margin-bottom: 60px;
    margin-left: 40px;
    margin-top: 50px;
}
    </style>
    <title></title>
</head>
<body>
    <form id="html" runat="server">
        <div id="body">
            Witaj!

            <br /> <br />
            ZTA Migartion App to aplikacja służąca wpomaganiu migracji systemów teleifnormatycznych zgodnie z architekturą Zero Thrust.

            <br /> <br />

            Aby zalogować się kliknij poniższy przycisk. 
        </div>

        <asp:Button runat="server" Text="Zaloguj" OnClick="GoToLoginPage" /> 
       
    </form>
    <footer class="border-top footer text-muted text-center">
        <div id="footer">
            Copyright &copy; Weronika Buras 2021
        </div>
    </footer>
    
</body>
</html>

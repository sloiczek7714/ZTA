<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="ZTA.UserPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profil użytkownika</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="CheckListButton" runat="server" Text="Przejdź do listy" OnClick="GoToCheckListPage" />
            </br> </br> 
            <asp:Button ID="ShowresultButton" runat="server" Text="Zobacz wczesniejsze wyniki" OnClick="GoToReportPage" />
            </br> </br> 
            <asp:Button ID="EditProfileButton" runat="server" Text="Pokaż dane" />
        </div>
    </form>
</body>
</html>

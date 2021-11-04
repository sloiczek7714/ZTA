<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="ZTA.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                Lista użytkowników
            </p>
            <br />
            <asp:Button runat="server" Text="Dodaj nowego użytkownika" OnClick="AddUser" />
            <br />
            <asp:SqlDataSource ID="ZTA" runat="server" ConnectionString="<%$ ConnectionStrings:ZTAConnectionString %>" SelectCommand="SELECT [ID], [email] FROM [Users]"></asp:SqlDataSource>
            <br />
            <asp:GridView ID="GridView" runat="server" DataSourceID="ZTA" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:BoundField HeaderText="email" DataField="email" />
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>

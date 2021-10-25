<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ZTA.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            Lista kontrolna 
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ZTA" Width="693px">
            <Columns>
                <asp:BoundField DataField="Numer_pytania" HeaderText="Numer" SortExpression="Numer_pytania" />
                <asp:BoundField DataField="Pytanie" HeaderText="Pytanie" SortExpression="Pytanie" />
                <asp:CheckBoxField HeaderText="Odpowiedź" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ZTA" runat="server" ConnectionString="<%$ ConnectionStrings:ZTAConnectionString %>" SelectCommand="SELECT [Numer pytania] AS Numer_pytania, [Pytanie] FROM [Question]"></asp:SqlDataSource>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckList.aspx.cs" Inherits="ZTA.WebForm1" %>

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


        <asp:SqlDataSource ID="ZTA" runat="server" ConnectionString="<%$ ConnectionStrings:ZTAConnectionString %>" SelectCommand="SELECT [Pytanie], [Numer pytania] AS Numer_pytania FROM [Question]" OnSelecting="ZTA_Selecting1"></asp:SqlDataSource>
        <br />
        <asp:GridView ID="GridView2" runat="server" DataSourceID="ZTA" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="Numer" DataField="Numer_pytania" />
                <asp:BoundField HeaderText="Pytanie" DataField="Pytanie" />
                <asp:TemplateField HeaderText="Odpowiedź">
                    <ItemTemplate>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatLayout="Table">
                            <asp:ListItem>Tak</asp:ListItem>
                            <asp:ListItem>Nie</asp:ListItem>
                            <asp:ListItem>Nie dotyczy</asp:ListItem>
                            <asp:ListItem>Potrzebny komentarz</asp:ListItem>
                        </asp:CheckBoxList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>

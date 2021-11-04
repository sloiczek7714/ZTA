<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckListPage.aspx.cs" Inherits="ZTA.CheckListPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista kontrolna </title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            Lista kontrolna 
        </p>


       <script type="text/javascript">
            function MutExChkList(chk) {
                var chkList = chk.parentNode.parentNode.parentNode;
                var chks = chkList.getElementsByTagName("input");
                for (var i = 0; i < chks.length; i++) {
                    if (chks[i] != chk && chk.checked) {
                        chks[i].checked = false;
                    }
                }
            }
        </script>



        <asp:SqlDataSource ID="ZTA" runat="server" ConnectionString="<%$ ConnectionStrings:ZTAConnectionString %>" SelectCommand="SELECT [Pytanie], [Numer pytania] AS Numer_pytania FROM [Question]" OnSelecting="ZTA_Selecting"></asp:SqlDataSource>
        <br />
        <asp:GridView ID="GridView" runat="server" DataSourceID="ZTA" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="Numer" DataField="Numer_pytania" />
                <asp:BoundField HeaderText="Pytanie" DataField="Pytanie" />
                <asp:TemplateField HeaderText="Odpowiedź">
                    <ItemTemplate>
                        <asp:CheckBoxList ID="CheckBoxList" runat="server" RepeatColumns="4" RepeatLayout="Table">
                            <asp:ListItem Text="Tak" onclick="MutExChkList(this);"> </asp:ListItem>
                            <asp:ListItem Text="Nie" onclick="MutExChkList(this);"></asp:ListItem>
                            <asp:ListItem Text="Nie dotyczy" onclick="MutExChkList(this);"></asp:ListItem>
                            <asp:ListItem Text="Potrzebny komentarz" onclick="MutExChkList(this);"></asp:ListItem>
                        </asp:CheckBoxList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>

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
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ZTA" Width="693px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Numer_pytania" HeaderText="Numer" SortExpression="Numer_pytania" />
                <asp:BoundField DataField="Pytanie" HeaderText="Pytanie" SortExpression="Pytanie" />
                <asp:TemplateField HeaderText="Odpowiedź">
                    <InsertItemTemplate>
                        <asp:DropDownList                             
                            id="answerList"                    
                    OnSelectedIndexChanged="SelectionChange"
                    runat="server">
                  <asp:ListItem Selected="Tak" Value="White"> White </asp:ListItem>
                  <asp:ListItem Value="Nie"> Silver </asp:ListItem>
                  <asp:ListItem Value="Nie dotyczy"> Dark Gray </asp:ListItem>
                  <asp:ListItem Value="Potrzebny komentarz"> Khaki </asp:ListItem>                
                            
                            </asp:DropDownList>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                    </InsertItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ZTA" runat="server" ConnectionString="<%$ ConnectionStrings:ZTAConnectionString %>" SelectCommand="SELECT [Numer pytania] AS Numer_pytania, [Pytanie] FROM [Question]"></asp:SqlDataSource>

        <asp:TemplateField HeaderText="Odpowiedź">
                    <InsertItemTemplate>
                        <asp:DropDownList                             
                            id="answerList"                    
                    OnSelectedIndexChanged="SelectionChange"
                    runat="server">
                  <asp:ListItem Selected="Tak" Value="White"> White </asp:ListItem>
                  <asp:ListItem Value="Nie"> Silver </asp:ListItem>
                  <asp:ListItem Value="Nie dotyczy"> Dark Gray </asp:ListItem>
                  <asp:ListItem Value="Potrzebny komentarz"> Khaki </asp:ListItem>                
                            
                            </asp:DropDownList>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                    </InsertItemTemplate>
                </asp:TemplateField>

    </form>
</body>
</html>

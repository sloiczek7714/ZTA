<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ZTA.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            1.	Czy istnieje zatwierdzona lista zasobów w sieci przedsiębiorstwa? Jeśli tak proszę przejść do punktu 2,  jeśli nie patrz punkt 1.1.

&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Tak" OnCheckedChanged="CheckBox1_CheckedChanged" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox2" runat="server" Text="Nie" OnClick="CheckBox2_CheckedChanged"  />
            &nbsp;&nbsp;&nbsp;
&nbsp;<asp:CheckBox ID="CheckBox3" runat="server" Text="Nie dotyczy" />
            &nbsp;&nbsp;&nbsp;
&nbsp;<asp:CheckBox ID="CheckBox4" runat="server" Text="Potrzebny komentarz" />
            <br />
            <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
            <br />
        </div>
        <br />
    </form>
</body>
</html>

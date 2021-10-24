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

&nbsp;&nbsp;<br />
            &nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Blanl" OnClick="showExplanation1" />
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Tak" InvokeOnClick="showExplanation1" OnCheckedChanged="showExplanation1" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox2" runat="server" Text="Nie" Click="showExplanation1"  />
            &nbsp;&nbsp;&nbsp;
&nbsp;<asp:CheckBox ID="CheckBox3" runat="server" Text="Nie dotyczy" OnClick="showExplanation1"/>
            &nbsp;&nbsp;&nbsp;
&nbsp;<asp:CheckBox ID="CheckBox4" runat="server" Text="Potrzebny komentarz" OnCheck="CheckBox4_CheckedChanged"  />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="1.1.	Należy przeanalizować źródła danych dostępnych w przedsiębiorstwie oraz usługi,      z których ono korzysta, a następnie sporządzić listę, które z nich będą uznawane za zasoby. Następnym krokiem jest zatwierdzenie listy oraz udostępnienie jej zgodnie z polityką bezpieczeństwa w przedsiębiorstwie. " Visible="false"></asp:Label>
            <br />
        </div>
        <br />
        <br />
        <br />
    </form>
</body>
</html>

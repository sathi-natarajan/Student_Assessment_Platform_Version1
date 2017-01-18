<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TeachersLoggedinPanel.ascx.vb" Inherits="StudentsAssessment.TeachersLoggedinPanel" %>

<table id="tblTeachersLoggedinPanel" cellpadding="2" cellspacing="0">
    <tr>
        <td><span id="spnTeacherName" runat="server" colspan="2" style="background-color:#000;color:#fff;"><strong>TEACHER NAME DISPLAYED</strong></span>&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkLogout" runat="server"  Font-Bold="true"  Font-Underline="false" ForeColor="White" BackColor="Black">LOG OUT</asp:LinkButton></td>
    </tr>
    <tr>
        <td><asp:LinkButton ID="lnkClass" runat="server" Font-Bold="true"  Font-Underline="false" ForeColor="White" BackColor="Black">CLASS</asp:LinkButton>&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkSubject" runat="server" Font-Bold="true"  Font-Underline="false" ForeColor="White" BackColor="Black">SUBJECT</asp:LinkButton>&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkEditProfile" runat="server" Font-Bold="true"  Font-Underline="false" ForeColor="White" BackColor="Black">EDIT PROFILE</asp:LinkButton></td>
    </tr>
    <tr>
        <td><asp:Label ID="lblAdminLoginMessage" runat="server" Forecolor="White" BackColor="Black">(Logged-in as administrator)</asp:Label></td>
    </tr>
</table>
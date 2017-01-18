<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeachersLogin1.aspx.vb" Inherits="StudentsAssessment.TeachersLogin" %>

<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="SiteFooter.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Teachers Interface - Teacher Log-in</title>
    <link href="Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
         <uc1:SiteHeader id="SiteHeader1" runat="server"></uc1:SiteHeader>
    <div id="Content">
        <div style="background-color:gray;">
        </div>
        <asp:Panel ID="pnlWelcome" runat="server" 
            cssclass="Loginpanel" HorizontalAlign="center"
             BackColor="LightGray" DefaultButton="">
            <div style="height:25px;background-color:mediumpurple;color:white;">
                <strong>LOGIN SCREEN</strong></div>
            <br />
            <br />
            <table id="tblLogin" cellpadding="3" cellspacing="3" border="0">
                <colgroup>
                    <col width="100px" />
                    <col width="200px" />
                </colgroup>
                <tr>
                    <td>User name: </td>
                    <td><asp:textbox ID="txtUsername" runat="server" MaxLength="25" Width="200px"></asp:textbox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter user name" ControlToValidate="txtUsername" Font-Bold="True" Font-Size="9px" ForeColor="Maroon"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>Password: </td>
                    <td><asp:textbox ID="txtPassword" TextMode="Password" runat="server" MaxLength="25" Width="200px"></asp:textbox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter the password" ControlToValidate="txtPassword" Font-Bold="True" Font-Size="9px" ForeColor="Maroon"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
            </table>
            <br />
            <br />  
             <div style="height:20px;background-color:darkgreen;">
                 <asp:LinkButton ID="lnkTeacherLogin" runat="server" Font-bold="true" Font-Underline="false" ForeColor="White">LOGIN</asp:LinkButton>
             </div>   
            <br /><br />
            <div style="height:20px;background-color:darkgreen;">
                <asp:LinkButton ID="lnkReturn" runat="server" Font-bold="true" Font-Underline="false" ForeColor="White">RETURN</asp:LinkButton>
            </div> 
            <div style="text-align:left;">
                <asp:Label ID="lblStatus" runat="server" CSSClass="Statusarea" Font-Bold="true"></asp:Label>
            </div>
        </asp:Panel>
    </div>
        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
        <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </form>
</body>
</html>

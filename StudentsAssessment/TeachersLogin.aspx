<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeachersLogin.aspx.vb" Inherits="StudentsAssessment.TeachersLogin" %>
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
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <asp:Panel runat="server" DefaultButton="lnkTeacherLogin">
                            <div id="tblLogin_boxaround">
                            <table id="tblLogin" border="0" cellspacing="5" cellpadding="0">
                                <caption>
                                    <div id="tblLogin_heading">
                                         <strong>LOGIN SCREEN</strong></div>
                                    </div>
                                </caption>
                                <tr>
                                    <td>Name</td>
                                    <td>
                                        <asp:TextBox ID="txtUsername" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" CssClass="Statusarea" ErrorMessage="Username is required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                                <tr>
                        <td>Password</td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" CssClass="Statusarea" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="height:20px;background-color:darkgreen;">
                                            <asp:LinkButton ID="lnkTeacherLogin" runat="server" Font-bold="true" Font-Underline="false" ForeColor="White">LOGIN</asp:LinkButton>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="height:20px;background-color:darkgreen;">
                                            <asp:LinkButton ID="lnkReturn" runat="server" Font-bold="true" Font-Underline="false" ForeColor="White">RETURN</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblStatus" runat="server" CSSClass="Statusarea" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                    </table>
                </div>
                        </asp:Panel>
  
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkTeacherLogin" />
                <asp:PostBackTrigger ControlID="lnkReturn" />
            </Triggers>
        </asp:UpdatePanel>    
    </div>
    <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
</form>
</body>
</html>
